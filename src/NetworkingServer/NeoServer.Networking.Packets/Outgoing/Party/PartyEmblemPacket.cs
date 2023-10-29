﻿using NeoServer.Game.Common.Contracts.Creatures;
using NeoServer.Game.Common.Creatures.Party;
using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Party;

public class PartyEmblemPacket : OutgoingPacket
{
    private readonly ICreature creature;
    private readonly PartyEmblem emblem;

    public PartyEmblemPacket(ICreature creature, PartyEmblem emblem)
    {
        this.creature = creature;
        this.emblem = emblem;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.CreatureEmblem);
        message.AddUInt32(creature.CreatureId);
        message.AddByte((byte)emblem);
    }
}