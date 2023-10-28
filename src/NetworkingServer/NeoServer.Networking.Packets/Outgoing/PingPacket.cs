namespace NeoServer.Networking.Packets.Outgoing;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

public class PingPacket : OutgoingPacket
{
    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.Ping);
    }
}