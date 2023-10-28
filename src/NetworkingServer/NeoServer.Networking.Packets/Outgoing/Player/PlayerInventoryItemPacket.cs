using NeoServer.Game.Common.Contracts.Creatures;
using NeoServer.Game.Common.Creatures.Players;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Player;

public class PlayerInventoryItemPacket : OutgoingPacket
{
    private readonly IInventory inventory;
    private readonly Slot slot;

    public PlayerInventoryItemPacket(IInventory inventory, Slot slot)
    {
        this.inventory = inventory;
        this.slot = slot;
    }

    public override void WriteToMessage(INetworkMessage message)
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
    }
}