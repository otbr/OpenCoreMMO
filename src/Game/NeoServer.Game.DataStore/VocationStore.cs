﻿using NeoServer.Game.Common.Contracts.Chats;
using NeoServer.Game.Common.Contracts.Creatures;
using NeoServer.Game.Common.Contracts.DataStores;

namespace NeoServer.Game.DataStore
{
    public class VocationStore : DataStore<VocationStore, byte, IVocation>, IVocationStore
    {
    }
}