﻿using Mediator;
using NeoServer.Game.Common.Contracts.Creatures;
using NeoServer.Game.Common.Contracts.Items;
using NeoServer.Game.Common.Contracts.World;
using NeoServer.Server.Common.Contracts;

namespace NeoServer.Application.Features.Trade.TradeRequest;

public record TradeRequestCommand(IPlayer Player, IItem Item, IPlayer SecondPlayer) : ICommand;

public class TradeRequestCommandHandler : ICommandHandler<TradeRequestCommand>
{
    private readonly SafeTradeSystem _tradeSystem;

    public TradeRequestCommandHandler(SafeTradeSystem tradeSystem, IMap map, IGameCreatureManager creatureManager)
    {
        _tradeSystem = tradeSystem;
    }

    public ValueTask<Unit> Handle(TradeRequestCommand command, CancellationToken cancellationToken)
    {
        command.Deconstruct(out var player, out var item, out var secondPlayer);

        _tradeSystem.Request(player, secondPlayer, item);
        return Unit.ValueTask;
    }
}