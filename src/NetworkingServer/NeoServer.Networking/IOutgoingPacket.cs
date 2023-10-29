using NeoServer.Networking.Messages;

namespace NeoServer.Networking;

public interface IOutgoingPacket
{
    void WriteToMessage(INetworkMessage message);
}