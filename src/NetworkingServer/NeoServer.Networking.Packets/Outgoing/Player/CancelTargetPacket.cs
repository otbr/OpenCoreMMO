namespace NeoServer.Networking.Packets.Outgoing.Player;

using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;

public class CancelTargetPacket : OutgoingPacket
{
    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.CancelTarget);
        message.AddUInt32(0x00);
    }
}