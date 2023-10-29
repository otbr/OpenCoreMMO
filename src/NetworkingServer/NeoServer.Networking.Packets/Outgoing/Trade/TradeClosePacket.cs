namespace NeoServer.Networking.Packets.Outgoing.Trade;

using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;

public class TradeClosePacket : OutgoingPacket
{
    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.TradeClose);
    }
}