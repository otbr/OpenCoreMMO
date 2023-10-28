using NeoServer.Game.Common.Location.Structs;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Incoming;

public class UseItemOnPacket : IncomingPacket
{
    public UseItemOnPacket(IReadOnlyNetworkMessage message)
    {
        Location = new Location(message.GetUInt16(), message.GetUInt16(), message.GetByte());
        ClientId = message.GetUInt16();
        StackPosition = message.GetByte();
        ToLocation = new Location(message.GetUInt16(), message.GetUInt16(), message.GetByte());
        ToClientId = message.GetUInt16();
        ToStackPosition = message.GetByte();
    }

    public Location Location { get; }
    public ushort ClientId { get; }
    public byte StackPosition { get; set; }
    public Location ToLocation { get; }
    public ushort ToClientId { get; }
    public byte ToStackPosition { get; set; }
}