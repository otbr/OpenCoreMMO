﻿using NeoServer.Networking.Packets.Outgoing;
using NeoServer.Server.Common.Contracts;
using NeoServer.Networking.Messages;
using NeoServer.Networking.Connection;

namespace NeoServer.Networking.Handlers.Player.Party;

public class PartyEnableSharedExperienceHandler : PacketHandler
{
    private readonly IGameServer _game;

    public PartyEnableSharedExperienceHandler(IGameServer game)
    {
        _game = game;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var experienceSharingActive = message.GetByte() == 1;
        if (!_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;
        if (player == null || player.PlayerParty.IsInParty == false) return;
        player.PlayerParty.Party.IsSharedExperienceEnabled = experienceSharingActive;
        connection.Send(new TextMessagePacket(
            $"Party experience sharing is now {(experienceSharingActive ? "enabled" : "disabled")}.",
            TextMessageOutgoingType.Small));
    }
}