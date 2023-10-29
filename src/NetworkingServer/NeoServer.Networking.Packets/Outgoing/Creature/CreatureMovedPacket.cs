using NeoServer.Game.Common.Location.Structs;
using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Creature;

public class CreatureMovedPacket : OutgoingPacket
{
    private readonly Location location;
    private readonly byte stackPosition;
    private readonly Location toLocation;

    public CreatureMovedPacket(Location location, Location toLocation, byte stackPosition)
    {
        this.location = location;
        this.toLocation = toLocation;
        this.stackPosition = stackPosition;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.CreatureMoved);

        message.AddLocation(location);
        message.AddByte(stackPosition);
        message.AddLocation(toLocation);
    }
}