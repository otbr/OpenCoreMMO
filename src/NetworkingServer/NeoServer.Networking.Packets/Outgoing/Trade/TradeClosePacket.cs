namespace NeoServer.Networking.Packets.Outgoing.Trade;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

public class TradeClosePacket : OutgoingPacket
{
    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.TradeClose);
    }
}