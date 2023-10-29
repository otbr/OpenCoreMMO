namespace NeoServer.Networking.Packets.Outgoing;

using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;

public class TextMessagePacket : OutgoingPacket
{
    private readonly string text;
    private readonly TextMessageOutgoingType type;

    public TextMessagePacket(string text, TextMessageOutgoingType type)
    {
        this.text = text;
        this.type = type;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.TextMessage);
        message.AddByte((byte)type);
        message.AddString(text);
    }
}