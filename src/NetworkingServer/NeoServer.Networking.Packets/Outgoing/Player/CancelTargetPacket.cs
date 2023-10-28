namespace NeoServer.Networking.Packets.Outgoing.Player;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

public class CancelTargetPacket : OutgoingPacket
{
    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.CancelTarget);
        message.AddUInt32(0x00);
    }
}