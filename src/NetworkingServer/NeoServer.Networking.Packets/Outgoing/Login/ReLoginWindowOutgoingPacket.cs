namespace NeoServer.Networking.Packets.Outgoing.Login;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

public class ReLoginWindowOutgoingPacket : OutgoingPacket
{
    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.ReLoginWindow);
    }
}