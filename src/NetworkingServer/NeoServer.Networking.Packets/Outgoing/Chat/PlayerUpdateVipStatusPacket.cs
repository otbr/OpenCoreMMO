namespace NeoServer.Networking.Packets.Outgoing.Chat;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;

public class PlayerUpdateVipStatusPacket : OutgoingPacket
{
    private readonly bool online;
    private readonly uint playerId;

    public PlayerUpdateVipStatusPacket(uint playerId, bool online)
    {
        this.playerId = playerId;
        this.online = online;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)(online
            ? STCPacketType.OnlineStatusVip
            : STCPacketType.OfflineStatusVip));
        message.AddUInt32(playerId);
    }
}