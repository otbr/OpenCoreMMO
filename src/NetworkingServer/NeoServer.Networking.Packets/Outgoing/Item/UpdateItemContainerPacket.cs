﻿using NeoServer.Game.Common.Contracts.Items;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Item;

public class UpdateItemContainerPacket : OutgoingPacket
{
    private readonly byte containerId;
    private readonly IItem item;
    public readonly byte slot;

    public UpdateItemContainerPacket(byte containerId, byte slot, IItem item)
    {
        this.containerId = containerId;
        this.item = item;
        this.slot = slot;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.ContainerUpdateItem);

        message.AddByte(containerId);
        message.AddByte(slot);
        message.AddUInt16(item.ClientId);
    }
}