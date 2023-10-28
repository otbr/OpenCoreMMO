namespace NeoServer.Networking.Packets.Outgoing.Chat;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

public class PlayerCloseChannelPacket : OutgoingPacket
{
    private readonly ushort channelId;

    public PlayerCloseChannelPacket(ushort channelId)
    {
        this.channelId = channelId;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.CloseChannel);
        message.AddUInt16(channelId);
    }
}