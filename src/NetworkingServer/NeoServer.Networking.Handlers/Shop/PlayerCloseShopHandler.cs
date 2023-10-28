using NeoServer.Server.Common.Contracts;
using NeoServer.Server.Tasks;
using NeoServer.Networking.Shared.Connection;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Handlers.Shop;

public class PlayerCloseShopHandler : PacketHandler
{
    private readonly IGameServer _game;

    public PlayerCloseShopHandler(IGameServer game)
    {
        _game = game;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        if (!_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;
        _game.Dispatcher.AddEvent(new Event(() => player.StopShopping()));
    }
}