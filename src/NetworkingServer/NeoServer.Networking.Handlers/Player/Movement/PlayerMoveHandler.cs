using NeoServer.Game.Common.Location;
using NeoServer.Server.Common.Contracts;
using NeoServer.Server.Tasks;
using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;
using NeoServer.Networking.Connection;

namespace NeoServer.Networking.Handlers.Player.Movement;

public class PlayerMoveHandler : PacketHandler
{
    private readonly IGameServer _game;

    public PlayerMoveHandler(IGameServer game)
    {
        _game = game;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var direction = ParseMovementPacket(message.IncomingPacket);

        if (_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player))
            _game.Dispatcher.AddEvent(new Event(() => player.WalkTo(direction)));
    }

    private Direction ParseMovementPacket(CTSPacketType walkPacket)
    {
        var direction = Direction.North;

        switch (walkPacket)
        {
            case CTSPacketType.WalkEast:
                direction = Direction.East;
                break;
            case CTSPacketType.WalkNorth:
                direction = Direction.North;
                break;
            case CTSPacketType.WalkSouth:
                direction = Direction.South;
                break;
            case CTSPacketType.WalkWest:
                direction = Direction.West;
                break;
            case CTSPacketType.WalkNorteast:
                direction = Direction.NorthEast;
                break;
            case CTSPacketType.WalkNorthwest:
                direction = Direction.NorthWest;
                break;
            case CTSPacketType.WalkSoutheast:
                direction = Direction.SouthEast;
                break;
            case CTSPacketType.WalkSouthwest:
                direction = Direction.SouthWest;
                break;
        }

        return direction;
    }
}