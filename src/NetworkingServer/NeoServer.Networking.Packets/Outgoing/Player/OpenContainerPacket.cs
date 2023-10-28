﻿using System;
using NeoServer.Game.Common.Contracts.Items.Types.Containers;
using NeoServer.Server.Common.Contracts.Network;

namespace NeoServer.Networking.Packets.Outgoing.Player;

public class OpenContainerPacket : OutgoingPacket
{
    private readonly IContainer container;
    private readonly byte containerId;

    public OpenContainerPacket(IContainer container, byte containerId)
    {
        this.container = container;
        this.containerId = containerId;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.ContainerOpen);

        message.AddByte(containerId);
        message.AddUInt16(container.ClientId);
        message.AddString(container.Name);
        message.AddByte(container.Capacity);

        message.AddByte(container.HasParent ? (byte)0x01 : (byte)0x00);

        message.AddByte(Math.Min((byte)0xFF, container.SlotsUsed));

        for (byte i = 0; i < container.SlotsUsed; i++) message.AddUInt16(container.Items[i].ClientId);
    }
}