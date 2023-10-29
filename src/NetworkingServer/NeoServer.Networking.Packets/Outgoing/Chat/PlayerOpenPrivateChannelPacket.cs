namespace NeoServer.Networking.Packets.Outgoing.Chat;

using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;

public class PlayerOpenPrivateChannelPacket : OutgoingPacket
{
    private readonly string receiver;

    public PlayerOpenPrivateChannelPacket(string receiver)
    {
        this.receiver = receiver;
    }

    //todo: this code is duplicated?
    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.OpenPrivateChannel);
        message.AddString(receiver);
    }
}