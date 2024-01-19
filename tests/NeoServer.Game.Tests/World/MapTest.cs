﻿using System;
using System.Collections.Generic;
using NeoServer.Game.Common.Contracts.Items;
using NeoServer.Game.Common.Location;
using NeoServer.Game.Common.Location.Structs;
using NeoServer.Game.Tests.Helpers;
using NeoServer.Game.World.Map;
using NeoServer.Game.World.Models.Tiles;

namespace NeoServer.Game.Tests.World;

public class MapTest
{
    public Map CreateMap(IItem item)
    {
        var world = new Game.World.World();

        for (var x = 100; x < 120; x++)
        for (var y = 100; y < 120; y++)
        {
            var items = new List<IItem>
            {
                ItemTestData.CreateRegularItem(1)
            };

            if (item.Location == new Location((ushort)x, (ushort)y, 7)) items.Add(item);

            world.AddTile(new DynamicTile(new Coordinate(x, y, 7), TileFlag.None, null, Array.Empty<IItem>(),
                items.ToArray()));
        }

        return new Map(world);
    }
}