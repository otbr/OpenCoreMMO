using NeoServer.Game.Common.Location.Structs;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Incoming;

public class LookAtPacket : IncomingPacket
{
    public LookAtPacket(IReadOnlyNetworkMessage message)
    {
        Location = new Location(message.GetUInt16(), message.GetUInt16(), message.GetByte());
        message.SkipBytes(2); //sprite id
        StackPosition = message.GetByte();
    }

    public Location Location { get; set; }
    public byte StackPosition { get; set; }
}