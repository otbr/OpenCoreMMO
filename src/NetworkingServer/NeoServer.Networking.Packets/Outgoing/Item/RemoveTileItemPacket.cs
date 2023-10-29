﻿using NeoServer.Game.Common.Contracts.Items;
using NeoServer.Game.Common.Helpers;
using NeoServer.Game.Common.Location.Structs;
using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Item;

public class RemoveTileItemPacket : OutgoingPacket
{
    public readonly IItem item;
    public readonly Location location;
    public readonly byte stackPosition;

    public RemoveTileItemPacket(Location location, byte stackPosition, IItem item)
    {
        if (item.IsNull()) return;

        this.location = location;
        this.stackPosition = stackPosition;
        this.item = item;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.AddAtStackPos);
        message.AddLocation(location);
        message.AddByte(stackPosition);
        message.AddUInt16(item.ClientId);
    }
}