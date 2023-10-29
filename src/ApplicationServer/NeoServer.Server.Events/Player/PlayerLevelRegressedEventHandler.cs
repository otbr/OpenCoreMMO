﻿using NeoServer.Game.Common.Creatures;
using NeoServer.Game.Common.Parsers;
using NeoServer.Networking.Packets.Outgoing;
using NeoServer.Server.Common.Contracts;
using NeoServer.Networking.Connection;

namespace NeoServer.Server.Events.Player;

public class PlayerLevelRegressedEventHandler : PlayerLevelChangeEventHandler
{
    public PlayerLevelRegressedEventHandler(IGameServer game) : base(game)
    {
    }

    protected override void SendLevelChangeMessage(SkillType skillType, IConnection connection, int fromLevel,
        int toLevel)
    {
        connection.OutgoingPackets.Enqueue(new TextMessagePacket(
            MessageParser.GetSkillRegressedMessage(skillType, fromLevel, toLevel),
            TextMessageOutgoingType.MESSAGE_EVENT_LEVEL_CHANGE));
    }
}