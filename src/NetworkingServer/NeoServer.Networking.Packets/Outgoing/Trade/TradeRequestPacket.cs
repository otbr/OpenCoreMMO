﻿using NeoServer.Game.Common.Contracts.Items;
using NeoServer.Networking.Shared;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Trade;

public class TradeRequestPacket : IOutgoingPacket
{
    public TradeRequestPacket(string playerName, IItem[] items, bool acknowledged = false)
    {
        PlayerName = playerName;
        Items = items;
        Acknowledged = acknowledged;
    }

    private string PlayerName { get; }
    private IItem[] Items { get; }
    private bool Acknowledged { get; }

    public void WriteToMessage(INetworkMessage message)
    {
        message.AddByte(Acknowledged
            ? (byte)STCPacketType.AcknowlegdeTradeRequest
            : (byte)STCPacketType.TradeRequest);

        message.AddString(PlayerName);

        message.AddByte((byte)Items.Length);
        foreach (var item in Items) message.AddUInt16(item.ClientId);
    }
}