using NeoServer.Game.Common.Contracts.Items;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Item;

public class AddTileItemPacket : OutgoingPacket
{
    private readonly IItem item;
    private readonly byte stackPosition;

    public AddTileItemPacket(IItem item, byte stackPosition)
    {
        this.item = item;
        this.stackPosition = stackPosition;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.AddAtStackPos);
        message.AddLocation(item.Location);
        message.AddByte(stackPosition);
        message.AddUInt16(item.ClientId);
    }
}