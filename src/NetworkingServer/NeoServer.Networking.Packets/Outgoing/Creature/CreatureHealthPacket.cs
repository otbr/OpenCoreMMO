﻿using System;
using NeoServer.Game.Common.Contracts.Creatures;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Creature;

public class CreatureHealthPacket : OutgoingPacket
{
    private readonly ICreature creature;

    public CreatureHealthPacket(ICreature creature)
    {
        this.creature = creature;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.CreatureHealth);

        message.AddUInt32(creature.CreatureId);

        if (creature.IsHealthHidden)
        {
            message.AddByte(0x00);
        }
        else
        {
            var result = (double)creature.HealthPoints / (int)Math.Max(creature.MaxHealthPoints, 1);
            result = Math.Ceiling(result * 100);

            message.AddByte((byte)result);
        }
    }
}