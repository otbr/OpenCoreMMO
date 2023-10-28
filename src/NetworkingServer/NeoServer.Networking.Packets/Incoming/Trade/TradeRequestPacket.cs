using NeoServer.Game.Common.Location.Structs;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Incoming.Trade;

public class TradeRequestPacket : IncomingPacket
{
    public TradeRequestPacket(IReadOnlyNetworkMessage message)
    {
        Location = new Location(message.GetUInt16(), message.GetUInt16(), message.GetByte());
        ClientId = message.GetUInt16();
        StackPosition = message.GetByte();
        PlayerId = message.GetUInt32();
    }

    public uint PlayerId { get; }
    public byte StackPosition { get; }
    public ushort ClientId { get; }
    public Location Location { get; }
}