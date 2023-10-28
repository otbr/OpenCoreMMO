﻿namespace NeoServer.Networking.Packets.Outgoing.Map;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

public class WorldLightPacket : OutgoingPacket
{
    private readonly byte Color;
    private readonly byte Level;

    public WorldLightPacket(byte level, byte color)
    {
        Level = level;
        Color = color;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.WorldLight);

        message.AddByte(Level);
        message.AddByte(Color);
    }
}