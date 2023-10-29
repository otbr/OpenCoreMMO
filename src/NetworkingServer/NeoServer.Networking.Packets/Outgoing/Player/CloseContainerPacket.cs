namespace NeoServer.Networking.Packets.Outgoing.Player;

using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;

public class CloseContainerPacket : OutgoingPacket
{
    private readonly byte containerId;

    public CloseContainerPacket(byte containerId)
    {
        this.containerId = containerId;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.ContainerClose);

        message.AddByte(containerId);
    }
}