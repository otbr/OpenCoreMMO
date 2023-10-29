using NeoServer.Networking.Connection;
using NeoServer.Networking.Messages;

namespace NeoServer.Networking;

public interface IPacketHandler
{
    void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection);
}