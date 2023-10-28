﻿using NeoServer.Game.Common.Contracts.Items;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Item;

public class RemoveItemContainerPacket : OutgoingPacket
{
    private readonly byte containerId;
    private readonly IItem item;
    private readonly byte slotIndex;

    public RemoveItemContainerPacket(byte containerId, byte slotIndex, IItem item)
    {
        this.containerId = containerId;
        this.slotIndex = slotIndex;
        this.item = item;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.ContainerRemoveItem);

        message.AddByte(containerId);

        message.AddByte(slotIndex);
    }
}