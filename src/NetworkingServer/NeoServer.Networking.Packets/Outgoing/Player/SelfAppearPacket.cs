﻿using NeoServer.Game.Common.Contracts.Creatures;
using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Player;

public class SelfAppearPacket : OutgoingPacket
{
    private readonly IPlayer player;

    public SelfAppearPacket(IPlayer player)
    {
        this.player = player;
    }

    private byte GraphicsSpeed => 0x32; //  beat duration (50)
    private byte CanReportBugs => 0x00;

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)STCPacketType.SelfAppear);

        message.AddUInt32(player.CreatureId);
        message.AddUInt16(GraphicsSpeed);
        message.AddByte(CanReportBugs); // can report bugs? todo: create tutor account type
    }
}