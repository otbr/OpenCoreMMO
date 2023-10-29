using System;
using System.Collections.Generic;
using NeoServer.Networking.Enums;
using NeoServer.Networking.Handlers.Chat;
using NeoServer.Networking.Handlers.LogIn;
using NeoServer.Networking.Handlers.Player;
using NeoServer.Networking.Handlers.Player.Movement;
using NeoServer.Networking.Handlers.Player.Party;
using NeoServer.Networking.Handlers.Shop;
using NeoServer.Networking.Handlers.Trade;

namespace NeoServer.Networking.Handlers;

public static class InputHandlerMap
{
    public static readonly IReadOnlyDictionary<CTSPacketType, Type> Data =
        new Dictionary<CTSPacketType, Type>
        {
            [CTSPacketType.PlayerLoginRequest] = typeof(AccountLoginHandler),
            [CTSPacketType.PlayerLogIn] = typeof(PlayerLogInHandler),
            [CTSPacketType.ChangeModes] = typeof(PlayerChangesModeHandler),
            [CTSPacketType.PlayerLogOut] = typeof(PlayerLogOutHandler),
            [CTSPacketType.StopAllActions] = typeof(StopAllActionsHandler),
            [CTSPacketType.WalkEast] = typeof(PlayerMoveHandler),
            [CTSPacketType.WalkWest] = typeof(PlayerMoveHandler),
            [CTSPacketType.WalkSouth] = typeof(PlayerMoveHandler),
            [CTSPacketType.WalkNorth] = typeof(PlayerMoveHandler),
            [CTSPacketType.WalkNorteast] = typeof(PlayerMoveHandler),
            [CTSPacketType.WalkNorthwest] = typeof(PlayerMoveHandler),
            [CTSPacketType.WalkSoutheast] = typeof(PlayerMoveHandler),
            [CTSPacketType.WalkSouthwest] = typeof(PlayerMoveHandler),
            [CTSPacketType.TurnEast] = typeof(PlayerTurnHandler),
            [CTSPacketType.TurnWest] = typeof(PlayerTurnHandler),
            [CTSPacketType.TurnNorth] = typeof(PlayerTurnHandler),
            [CTSPacketType.TurnSouth] = typeof(PlayerTurnHandler),
            [CTSPacketType.AutoMove] = typeof(PlayerAutoWalkHandler),
            [CTSPacketType.Ping] = typeof(PlayerPingResponseHandler),
            [CTSPacketType.CancelAutoWalk] = typeof(PlayerCancelAutoWalkHandler),
            [CTSPacketType.ItemUse] = typeof(PlayerUseItemHandler),
            [CTSPacketType.ItemUseOn] = typeof(PlayerUseOnItemHandler),
            [CTSPacketType.ItemUseOnCreature] = typeof(PlayerUseOnCreatureHandler),
            [CTSPacketType.ContainerClose] = typeof(PlayerCloseContainerHandler),
            [CTSPacketType.ContainerUp] = typeof(PlayerGoBackContainerHandler),
            [CTSPacketType.ItemThrow] = typeof(PlayerThrowItemHandler),
            [CTSPacketType.Attack] = typeof(PlayerAttackHandler),
            [CTSPacketType.LookAt] = typeof(PlayerLookAtHandler),
            [CTSPacketType.Speech] = typeof(PlayerSayHandler),
            [CTSPacketType.ChannelOpenPrivate] = typeof(PlayerOpenPrivateChannelHandler),
            [CTSPacketType.ChannelListRequest] = typeof(PlayerChannelListRequestHandler),
            [CTSPacketType.ChannelOpen] = typeof(PlayerOpenChannelHandler),
            [CTSPacketType.ChannelClose] = typeof(PlayerCloseChannelHandler),
            [CTSPacketType.AddVip] = typeof(PlayerAddVipHandler),
            [CTSPacketType.RemoveVip] = typeof(PlayerRemoveVipHandler),
            [CTSPacketType.NpcChannelClose] = typeof(PlayerCloseNpcChannelHandler),
            [CTSPacketType.CloseShop] = typeof(PlayerCloseShopHandler),
            [CTSPacketType.PlayerSale] = typeof(PlayerSaleHandler),
            [CTSPacketType.PlayerPurchase] = typeof(PlayerPurchaseHandler),
            [CTSPacketType.PartyInvite] = typeof(PlayerInviteToPartyHandler),
            [CTSPacketType.PartyRevoke] = typeof(PlayerRevokeInvitePartyHandler),
            [CTSPacketType.PartyJoin] = typeof(PlayerJoinPartyHandler),
            [CTSPacketType.PartyLeave] = typeof(PlayerLeavePartyHandler),
            [CTSPacketType.PartyPassLeadership] = typeof(PlayerPassPartyLeadershipHandler),
            [CTSPacketType.EnableSharedExp] = typeof(PartyEnableSharedExperienceHandler),
            [CTSPacketType.WindowText] = typeof(PlayerWriteEventHandler),
            [CTSPacketType.OutfitChangeRequest] = typeof(PlayerRequestOutFitHandler),
            [CTSPacketType.OutfitChangeCompleted] = typeof(PlayerChangeCompletedOutFitHandler),
            [CTSPacketType.TradeRequest] = typeof(TradeRequestHandler),
            [CTSPacketType.TradeCancel] = typeof(TradeCancelHandler),
            [CTSPacketType.TradeAccept] = typeof(TradeAcceptHandler)
        };
}