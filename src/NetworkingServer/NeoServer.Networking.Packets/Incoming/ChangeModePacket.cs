﻿using NeoServer.Game.Common.Creatures.Players;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Incoming;

public class ChangeModePacket : IncomingPacket
{
    public ChangeModePacket(IReadOnlyNetworkMessage message)
    {
        FightMode = (FightMode)message.GetByte();
        ChaseMode = (ChaseMode)message.GetByte();
        SecureMode = message.GetByte();
    }

    public FightMode FightMode { get; }
    public ChaseMode ChaseMode { get; }
    public byte SecureMode { get; }
}