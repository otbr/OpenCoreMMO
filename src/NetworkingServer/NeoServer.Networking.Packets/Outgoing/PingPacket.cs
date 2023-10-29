namespace NeoServer.Networking.Packets.Outgoing;

using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;

public class PingPacket : OutgoingPacket
{
    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.Ping);
    }
}