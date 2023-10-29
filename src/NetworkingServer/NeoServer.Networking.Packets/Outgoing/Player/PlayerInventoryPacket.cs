using System;
using NeoServer.Game.Common.Contracts.Creatures;
using NeoServer.Game.Common.Creatures.Players;
using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Player;

public class PlayerInventoryPacket : OutgoingPacket
{
    private readonly IInventory inventory;

    public PlayerInventoryPacket(IInventory inventory)
    {
        this.inventory = inventory;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        var addInventoryItem = new Action<Slot>(slot =>
        {
            if (inventory[slot] == null)
            {
                message.AddByte((byte)STCPacketType.InventoryEmpty);
                message.AddByte((byte)slot);
            }
            else
            {
                message.AddByte((byte)STCPacketType.InventoryItem);
                message.AddByte((byte)slot);
                message.AddUInt16(inventory[slot].ClientId);
            }
        });

        addInventoryItem(Slot.Head);
        addInventoryItem(Slot.Necklace);
        addInventoryItem(Slot.Backpack);
        addInventoryItem(Slot.Body);
        addInventoryItem(Slot.Right);
        addInventoryItem(Slot.Left);
        addInventoryItem(Slot.Legs);
        addInventoryItem(Slot.Feet);
        addInventoryItem(Slot.Ring);
        addInventoryItem(Slot.Ammo);
    }
}