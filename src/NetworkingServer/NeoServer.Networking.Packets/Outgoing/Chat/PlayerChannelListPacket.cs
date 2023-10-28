﻿using NeoServer.Game.Common.Contracts.Chats;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Chat;

public class PlayerChannelListPacket : OutgoingPacket
{
    private readonly IChatChannel[] chatChannels;

    public PlayerChannelListPacket(IChatChannel[] chatChannels)
    {
        this.chatChannels = chatChannels;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.ChannelList);

        message.AddByte((byte)chatChannels.Length);

        foreach (var channel in chatChannels)
        {
            message.AddUInt16(channel.Id);
            message.AddString(channel.Name);
        }
    }
}