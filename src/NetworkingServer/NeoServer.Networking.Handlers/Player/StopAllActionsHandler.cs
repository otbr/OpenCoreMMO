using NeoServer.Server.Common.Contracts;
using NeoServer.Server.Tasks;
using NeoServer.Networking.Shared.Connection;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Handlers.Player;

public class StopAllActionsHandler : PacketHandler
{
    private readonly IGameServer _game;

    public StopAllActionsHandler(IGameServer game)
    {
        _game = game;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        if (_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player))
            _game.Dispatcher.AddEvent(new Event(() => player.StopAllActions()));
    }
}