using NeoServer.Game.Common.Contracts.Creatures;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Player;

public class PlayerWalkCancelPacket : OutgoingPacket
{
    private readonly IPlayer player;

    public PlayerWalkCancelPacket(IPlayer player)
    {
        this.player = player;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.PlayerWalkCancel);
        message.AddByte((byte)player.Direction);
    }
}