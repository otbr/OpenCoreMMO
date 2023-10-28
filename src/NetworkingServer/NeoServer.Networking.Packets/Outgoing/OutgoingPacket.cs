using NeoServer.Networking.Shared;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Outgoing;

public abstract class OutgoingPacket : IOutgoingPacket
{
    public virtual bool Disconnect { get; protected set; } = false;
    public abstract void WriteToMessage(INetworkMessage message);
}