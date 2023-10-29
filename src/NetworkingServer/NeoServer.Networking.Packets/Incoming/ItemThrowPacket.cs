using NeoServer.Game.Common.Location.Structs;
using NeoServer.Networking.Messages;

namespace NeoServer.Networking.Packets.Incoming;

public class ItemThrowPacket : IncomingPacket
{
    public ItemThrowPacket(IReadOnlyNetworkMessage message)
    {
        FromLocation = new Location(message.GetUInt16(), message.GetUInt16(), message.GetByte());
        ItemClientId = message.GetUInt16();
        FromStackPosition = message.GetByte();
        ToLocation = new Location(message.GetUInt16(), message.GetUInt16(), message.GetByte());
        Count = message.GetByte();
    }

    public Location FromLocation { get; set; }
    public ushort ItemClientId { get; set; }
    public byte FromStackPosition { get; set; }
    public Location ToLocation { get; set; }
    public byte Count { get; set; }
}