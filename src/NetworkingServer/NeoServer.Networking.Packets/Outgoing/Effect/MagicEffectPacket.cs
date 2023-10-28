using NeoServer.Game.Common.Creatures;
using NeoServer.Game.Common.Location.Structs;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Effect;

public class MagicEffectPacket : OutgoingPacket
{
    private readonly EffectT effect;
    private readonly Location location;

    public MagicEffectPacket(Location location, EffectT effect)
    {
        this.location = location;
        this.effect = effect;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.MagicEffect);
        message.AddLocation(location);
        message.AddByte((byte)effect);
    }
}