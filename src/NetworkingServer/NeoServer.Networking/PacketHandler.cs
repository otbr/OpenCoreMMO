using NeoServer.Networking.Connection;
using NeoServer.Networking.Messages;

namespace NeoServer.Networking;

public abstract class PacketHandler : IPacketHandler
{
    public abstract void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection);
}