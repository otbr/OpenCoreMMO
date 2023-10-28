namespace NeoServer.Networking.Packets.Outgoing.Player;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

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