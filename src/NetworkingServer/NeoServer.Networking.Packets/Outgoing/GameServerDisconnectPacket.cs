namespace NeoServer.Networking.Packets.Outgoing;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

public sealed class GameServerDisconnectPacket : OutgoingPacket
{
    private readonly string reason;

    public GameServerDisconnectPacket(string reason)
    {
        this.reason = reason;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.Disconnect);
        message.AddString(reason);
    }
}