﻿using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Incoming.Chat;

public class AddVipPacket : IncomingPacket
{
    public AddVipPacket(IReadOnlyNetworkMessage message)
    {
        Name = message.GetString();
    }

    public string Name { get; set; }
}