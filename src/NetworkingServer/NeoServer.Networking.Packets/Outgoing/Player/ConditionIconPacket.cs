namespace NeoServer.Networking.Packets.Outgoing.Player;

using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;

public class ConditionIconPacket : OutgoingPacket
{
    private readonly ushort icons;

    public ConditionIconPacket(ushort icons)
    {
        this.icons = icons;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.PlayerConditions);
        message.AddUInt16(icons);
    }
}