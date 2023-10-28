using NeoServer.Game.Common.Contracts.Items;
using NeoServer.Server.Common.Contracts.Network;

namespace NeoServer.Networking.Packets.Outgoing.Item;

public class AddItemContainerPacket : OutgoingPacket
{
    private readonly byte containerId;
    private readonly IItem item;

    public AddItemContainerPacket(byte containerId, IItem item)
    {
        this.containerId = containerId;
        this.item = item;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.ContainerAddItem);

        message.AddByte(containerId);
        message.AddUInt16(item.ClientId);
    }
}