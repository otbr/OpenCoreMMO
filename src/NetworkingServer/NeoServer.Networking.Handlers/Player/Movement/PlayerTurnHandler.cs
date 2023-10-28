using NeoServer.Game.Common.Location;
using NeoServer.Server.Common.Contracts;
using NeoServer.Server.Tasks;
using NeoServer.Networking.Shared.Connection;
using NeoServer.Networking.Shared.Messages;
using NeoServer.Networking.Shared.Enums;

namespace NeoServer.Networking.Handlers.Player.Movement;

public class PlayerTurnHandler : PacketHandler
{
    private readonly IGameServer _game;

    public PlayerTurnHandler(IGameServer game)
    {
        _game = game;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var direction = ParseTurnPacket(message.IncomingPacket);

        if (!_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;

        _game.Dispatcher.AddEvent(new Event(() => player.TurnTo(direction)));
    }

    private Direction ParseTurnPacket(CTSPacketType turnPacket)
    {
        var direction = Direction.North;

        switch (turnPacket)
        {
            case CTSPacketType.TurnNorth:
                direction = Direction.North;
                break;
            case CTSPacketType.TurnEast:
                direction = Direction.East;
                break;
            case CTSPacketType.TurnSouth:
                direction = Direction.South;
                break;
            case CTSPacketType.TurnWest:
                direction = Direction.West;
                break;
        }

        return direction;
    }
}