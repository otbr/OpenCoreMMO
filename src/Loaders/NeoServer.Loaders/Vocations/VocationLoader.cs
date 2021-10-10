﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using NeoServer.Game.Common;
using NeoServer.Game.Common.Contracts.Creatures;
using NeoServer.Game.Common.Contracts.DataStores;
using NeoServer.Game.Creatures.Vocations;
using NeoServer.Game.DataStore;
using NeoServer.Server.Configurations;
using NeoServer.Server.Helpers.Extensions;
using NeoServer.Server.Helpers.JsonConverters;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core;

namespace NeoServer.Loaders.Vocations
{
    public class VocationLoader
    {
        private readonly GameConfiguration gameConfiguration;

        private readonly ILogger logger;
        private readonly ServerConfiguration serverConfiguration;
        private readonly IVocationStore _vocationStore;

        public VocationLoader(GameConfiguration gameConfiguration, ILogger logger,
            ServerConfiguration serverConfiguration, IVocationStore vocationStore)
        {
            this.gameConfiguration = gameConfiguration;
            this.logger = logger;
            this.serverConfiguration = serverConfiguration;
            _vocationStore = vocationStore;
        }

        public void Load()
        {
            logger.Step("Loading vocations...", "{n} vocations loaded", () =>
            {
                var vocations = GetVocations();
                foreach (var vocation in vocations)
                {
                    _vocationStore.Add(vocation.VocationType, vocation);
                }
                return new object[] {vocations.Count};
            });
        }

        private List<Vocation> GetVocations()
        {
            var basePath = $"{serverConfiguration.Data}";
            var jsonString = File.ReadAllText(Path.Combine(basePath, "vocations.json"));
            var vocations = JsonConvert.DeserializeObject<List<Vocation>>(jsonString, new JsonSerializerSettings
            {
                Converters =
                {
                    new AbstractConverter<VocationFormula, IVocationFormula>(),
                    new SkillConverter()
                }
            });
            return vocations;
        }

        public class SkillConverter : JsonConverter<Dictionary<byte, float>>
        {
            public override Dictionary<byte, float> ReadJson(JsonReader reader, Type objectType,
                [AllowNull] Dictionary<byte, float> existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                return serializer.Deserialize<List<Dictionary<string, string>>>(reader).ToDictionary(
                    x => byte.Parse(x["id"]),
                    x => float.Parse(x["multiplier"], CultureInfo.InvariantCulture.NumberFormat));
            }

            public override void WriteJson(JsonWriter writer, [AllowNull] Dictionary<byte, float> value,
                JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}