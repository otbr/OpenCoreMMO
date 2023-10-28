using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Shared;

public interface IOutgoingPacket
{
    void WriteToMessage(INetworkMessage message);
}