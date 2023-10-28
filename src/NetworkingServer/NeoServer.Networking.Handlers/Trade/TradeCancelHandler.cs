using NeoServer.Game.Systems.SafeTrade;
using NeoServer.Server.Common.Contracts;
using NeoServer.Server.Tasks;
using NeoServer.Networking.Shared.Connection;
using NeoServer.Networking.Shared.Messages;

namespace NeoServer.Networking.Handlers.Trade;

public class TradeCancelHandler : PacketHandler
{
    private readonly IGameServer _gameServer;
    private readonly SafeTradeSystem _tradeSystem;

    public TradeCancelHandler(SafeTradeSystem tradeSystem, IGameServer gameServer)
    {
        _tradeSystem = tradeSystem;
        _gameServer = gameServer;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        if (!_gameServer.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;

        if (player is null) return;

        _gameServer.Dispatcher.AddEvent(new Event(() =>
            _tradeSystem.Cancel(player)));
    }
}