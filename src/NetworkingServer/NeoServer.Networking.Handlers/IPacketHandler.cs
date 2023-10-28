using NeoServer.Networking.Shared.Connection;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Handlers;

public interface IPacketHandler
{
    void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection);
}