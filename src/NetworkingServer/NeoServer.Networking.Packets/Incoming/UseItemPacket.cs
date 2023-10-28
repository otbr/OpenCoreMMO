using NeoServer.Game.Common.Location.Structs;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Incoming;

public class UseItemPacket : IncomingPacket
{
    public UseItemPacket(IReadOnlyNetworkMessage message)
    {
        Location = new Location(message.GetUInt16(), message.GetUInt16(), message.GetByte());
        ClientId = message.GetUInt16();
        StackPosition = message.GetByte();
        Index = message.GetByte();
    }

    public Location Location { get; }
    public ushort ClientId { get; }
    public byte StackPosition { get; set; }
    public byte Index { get; set; }
}