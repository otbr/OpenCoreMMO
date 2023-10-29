using NeoServer.Game.Common.Chats;
using NeoServer.Networking.Messages;
using NeoServer.Networking.Packets.Outgoing.Chat;

namespace NeoServer.Networking.Packets.Client
{
    public static class STCPacketExtensions
    {
        public static void Read(this MessageToChannelSTCPacket packet, INetworkMessage networkMessage)
        {
            packet.None = networkMessage.GetUInt32();
            packet.Name = networkMessage.GetString();

            if (packet.SpeechType != SpeechType.ChannelRed2Text)
                packet.Level = networkMessage.GetUInt16();

            packet.SpeechType = (SpeechType)networkMessage.GetByte();
            packet.ChannelId = networkMessage.GetUInt16();
            packet.Message = networkMessage.GetString();
        }
    }
}