namespace NeoServer.Networking.Packets.Outgoing.Chat;

using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;

public class PlayerOpenChannelPacket : OutgoingPacket
{
    private readonly ushort channelId;
    private readonly string name;

    public PlayerOpenChannelPacket(ushort channelId, string name)
    {
        this.channelId = channelId;
        this.name = name;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.OpenChannel);
        message.AddUInt16(channelId);
        message.AddString(name);
    }
}