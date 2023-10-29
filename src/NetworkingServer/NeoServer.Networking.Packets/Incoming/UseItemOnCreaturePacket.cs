using NeoServer.Game.Common.Location.Structs;
using NeoServer.Networking.Messages;

namespace NeoServer.Networking.Packets.Incoming;

public class UseItemOnCreaturePacket : IncomingPacket
{
    public UseItemOnCreaturePacket(IReadOnlyNetworkMessage message)
    {
        FromLocation = new Location(message.GetUInt16(), message.GetUInt16(), message.GetByte());
        ClientId = message.GetUInt16();
        FromStackPosition = message.GetByte();
        CreatureId = message.GetUInt32();
    }

    public Location FromLocation { get; }
    public ushort ClientId { get; }
    public byte FromStackPosition { get; set; }
    public uint CreatureId { get; }
}