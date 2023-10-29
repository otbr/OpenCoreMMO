using NeoServer.Game.Common.Chats;
using NeoServer.Game.Common.Contracts.Chats;
using NeoServer.Game.Common.Contracts.Creatures;
using NeoServer.Networking.Packets.Outgoing.Chat;
using NeoServer.Server.Common.Contracts;

namespace NeoServer.Server.Events.Chat;

public class ChatMessageAddedEventHandler
{
    private readonly IGameServer game;

    public ChatMessageAddedEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(ISociableCreature player, IChatChannel chatChannel, SpeechType speechType, string message)
    {
        if (chatChannel is null) return;
        if (string.IsNullOrWhiteSpace(message)) return;

        foreach (var user in chatChannel.Users)
        {
            if (!game.CreatureManager.GetPlayerConnection(user.Player.CreatureId, out var connection)) continue;

            //Actual
            //var oldPacket = new MessageToChannelPacket(player, speechType, message, chatChannel.Id);
            //connection.OutgoingPackets.Enqueue(oldPacket);

            //New
            if (speechType == SpeechType.None) return;
            if (string.IsNullOrWhiteSpace(message)) return;
            if (chatChannel.Id == default) return;

            var newPacket = new MessageToChannelSTCPacket(player.GetPlayerName(), player.GetPlayerLevel(), speechType, message, chatChannel.Id);

            connection.OutgoingPackets.Enqueue(newPacket);

            connection.Send();
        }
    }
}

public static class CreatureExtensions
{
    public static string GetPlayerName(this ICreature creature)
        => creature != null ? creature.Name : string.Empty;

    public static ushort GetPlayerLevel(this ICreature creature)
        => creature != null && creature is IPlayer ? ((IPlayer)creature).Level : (ushort)0;
}