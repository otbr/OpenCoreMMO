namespace NeoServer.Networking.Packets.Outgoing.Login;

using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;

public class ReLoginWindowOutgoingPacket : OutgoingPacket
{
    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.ReLoginWindow);
    }
}