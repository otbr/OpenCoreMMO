﻿using NeoServer.Networking.Messages;

namespace NeoServer.Networking.Packets.Incoming.Chat;

public class OpenPrivateChannelPacket : IncomingPacket
{
    public OpenPrivateChannelPacket(IReadOnlyNetworkMessage message)
    {
        Receiver = message.GetString();
    }

    public string Receiver { get; }
}