﻿using NeoServer.Server.Commands.Player;
using NeoServer.Server.Common.Contracts;
using NeoServer.Server.Tasks;
using NeoServer.Networking.Messages;
using NeoServer.Networking.Connection;

namespace NeoServer.Networking.Handlers.Player;

public class PlayerLogOutHandler : PacketHandler
{
    private readonly IGameServer _game;
    private readonly PlayerLogOutCommand _playerLogOutCommand;

    public PlayerLogOutHandler(IGameServer game, PlayerLogOutCommand playerLogOutCommand)
    {
        _game = game;
        _playerLogOutCommand = playerLogOutCommand;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        if (_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player))
            _game.Dispatcher.AddEvent(new Event(() => _playerLogOutCommand.Execute(player)));
    }
}