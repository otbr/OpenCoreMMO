using NeoServer.Game.Common.Chats;
using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;

namespace NeoServer.Networking.Packets.Outgoing.Chat;

//public class MessageToChannelPacket : OutgoingPacket
//{
//    public MessageToChannelPacket(ICreature from, SpeechType talkType, string message, ushort channelId)
//    {
//        From = from;
//        TalkType = talkType;
//        Message = message;
//        ChannelId = channelId;
//    }

//    public ICreature From { get; }
//    public SpeechType TalkType { get; }
//    public string Message { get; }
//    public ushort ChannelId { get; }

//    public override void WriteToMessage(INetworkMessage message)
//    {
//        if (TalkType == SpeechType.None) return;
//        if (string.IsNullOrWhiteSpace(Message)) return;
//        if (ChannelId == default) return;

//        message.AddByte((byte)STCPacketType.SendPrivateMessage);
//        message.AddUInt32(0x00);

//        var speechType = TalkType;

//        if (speechType == SpeechType.ChannelRed2Text)
//        {
//            message.AddString(string.Empty);
//            speechType = SpeechType.ChannelRed1Text;
//        }
//        else
//        {
//            if (From is not null)
//                message.AddString(From.Name);
//            else
//                message.AddString(string.Empty);
//            //Add level only for players
//            if (From is IPlayer player)
//                message.AddUInt16(player.Level);
//            else
//                message.AddUInt16(0x00);
//        }

//        message.AddByte((byte)speechType);
//        message.AddUInt16(ChannelId);
//        message.AddString(Message);
//    }
//}

public abstract class STCPacket : IOutgoingPacket
{
    #region Properties

    public abstract STCPacketType PacketType { get; }
    public abstract void WriteToMessage(INetworkMessage message);

    #endregion
}

public class MessageToChannelSTCPacket : STCPacket
{
    public override STCPacketType PacketType => STCPacketType.SendPrivateMessage;
    public uint None { get; set; }
    public string Name { get; set; }
    public ushort Level { get; set; }
    public SpeechType SpeechType { get; set; }
    public ushort ChannelId { get; set; }
    public string Message { get; set; }

    public MessageToChannelSTCPacket()
    {
            
    }

    public MessageToChannelSTCPacket(string name, ushort level, SpeechType speechType, string message, ushort channelId)
    {
        None = 0;
        Name = name;
        Level = level;
        SpeechType = speechType;
        ChannelId = channelId;
        Message = message;
    }

    public override void WriteToMessage(INetworkMessage networkMessage)
    {
        networkMessage.AddByte((byte)PacketType);
        networkMessage.AddUInt32(None);
        networkMessage.AddString(Name);

        if (SpeechType == SpeechType.ChannelRed2Text)
            SpeechType = SpeechType.ChannelRed1Text;

        if (SpeechType != SpeechType.ChannelRed2Text)
            networkMessage.AddUInt16(Level);

        networkMessage.AddByte((byte)SpeechType);
        networkMessage.AddUInt16(ChannelId);
        networkMessage.AddString(Message);
    }
}