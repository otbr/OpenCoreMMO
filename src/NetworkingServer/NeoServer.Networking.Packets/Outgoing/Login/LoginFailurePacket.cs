namespace NeoServer.Networking.Packets.Outgoing.Login;

using NeoServer.Networking.Messages;

public class LoginFailurePacket : OutgoingPacket
{
    private readonly string _text;

    public LoginFailurePacket(string text)
    {
        _text = text;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte(0x0A);
        message.AddString(_text);
    }
}