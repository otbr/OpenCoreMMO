using NeoServer.Game.Common.Contracts.Creatures;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Creature;

public class CreatureLightPacket : OutgoingPacket
{
    private readonly IPlayer player;

    public CreatureLightPacket(IPlayer player)
    {
        this.player = player;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.CreatureLight);

        message.AddUInt32(player.CreatureId);
        message.AddByte(player.LightBrightness); // light level
        message.AddByte(player.LightColor); // color
    }
}