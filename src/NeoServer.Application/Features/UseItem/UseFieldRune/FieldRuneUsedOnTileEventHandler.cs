using Mediator;
using NeoServer.Game.Common.Contracts.World;
using NeoServer.Game.Item.Items.UsableItems.Runes.Events;
using NeoServer.Networking.Packets.Outgoing.Effect;
using NeoServer.Server.Common.Contracts;

namespace NeoServer.Application.Features.UseItem.UseFieldRune;

public class FieldRuneUsedOnTileEventHandler : INotificationHandler<FieldRuneUsedOnTileEvent>
{
    private readonly IMap _map;
    private readonly IGameCreatureManager _creatureManager;

    public FieldRuneUsedOnTileEventHandler(IMap map, IGameCreatureManager creatureManager)
    {
        _map = map;
        _creatureManager = creatureManager;
    }

    public ValueTask Handle(FieldRuneUsedOnTileEvent notification, CancellationToken cancellationToken)
    {
        var usedBy = notification.Player;
        var item = notification.Rune;
        var onTile = notification.OnTile;

        foreach (var spectator in _map.GetPlayersAtPositionZone(usedBy.Location))
        {
            if (!_creatureManager.GetPlayerConnection(spectator.CreatureId, out var connection)) continue;

            if (item.Metadata.ShootType != default)
                connection.OutgoingPackets.Enqueue(new DistanceEffectPacket(usedBy.Location, onTile.Location,
                    (byte)item.Metadata.ShootType));
            connection.Send();
        }

        return ValueTask.CompletedTask;
    }
}