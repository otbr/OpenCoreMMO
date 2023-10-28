﻿using NeoServer.Game.Common.Location.Structs;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Effect;

public class DistanceEffectPacket : OutgoingPacket
{
    private readonly byte effect;
    private readonly Location location;
    private readonly Location toLocation;

    public DistanceEffectPacket(Location location, Location toLocation, byte effect)
    {
        this.location = location;
        this.toLocation = toLocation;
        this.effect = effect;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.DistanceShootEffect);
        message.AddLocation(location);
        message.AddLocation(toLocation);
        message.AddByte(effect);
    }
}