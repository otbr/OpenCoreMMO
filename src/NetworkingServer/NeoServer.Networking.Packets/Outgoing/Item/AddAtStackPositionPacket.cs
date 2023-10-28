﻿using NeoServer.Game.Common.Contracts.Creatures;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Item;

public class AddAtStackPositionPacket : OutgoingPacket
{
    private readonly ICreature creature;
    private readonly byte stackPosition;

    public AddAtStackPositionPacket(ICreature creature, byte stackPosition)
    {
        this.creature = creature;
        this.stackPosition = stackPosition;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.AddAtStackPos);
        message.AddLocation(creature.Location);
        message.AddByte(stackPosition);
    }
}