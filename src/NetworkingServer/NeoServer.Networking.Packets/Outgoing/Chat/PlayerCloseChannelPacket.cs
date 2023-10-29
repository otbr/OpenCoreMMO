namespace NeoServer.Networking.Packets.Outgoing.Chat;

using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;

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