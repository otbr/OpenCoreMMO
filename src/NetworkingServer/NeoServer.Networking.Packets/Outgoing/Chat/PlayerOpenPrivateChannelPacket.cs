namespace NeoServer.Networking.Packets.Outgoing.Chat;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

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