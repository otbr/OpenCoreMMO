using NeoServer.Networking.Shared.Enums;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NeoServer.Networking.Shared.Messages;

public class ReadOnlyNetworkMessage : IReadOnlyNetworkMessage
{
    private static readonly byte[] EmptyBuffer = Array.Empty<byte>();
    private static readonly Encoding Iso88591Encoding = Encoding.GetEncoding("iso-8859-1");

    public ReadOnlyNetworkMessage(byte[] buffer, int length)
    {
        if (buffer == null) return;

        Buffer = buffer;
        Length = length;
        BytesRead = 0;
    }

    public byte[] Buffer { get; protected set; }
    public int Length { get; protected set; }

    /// <summary>
    ///     Get the message's buffer
    /// </summary>
    public ReadOnlySpan<byte> GetMessageInBytes()
    {
        return Length < 0 ? EmptyBuffer : Length == 0 ? Buffer : Buffer[..Length];
    }

    public int BytesRead { get; private set; }

    public CTSPacketType IncomingPacket { get; private set; } = CTSPacketType.None;

    public CTSPacketType GetIncomingPacketType(bool isAuthenticated)
    {
        switch (isAuthenticated)
        {
            case true:
                if (Buffer.Length < 9) return CTSPacketType.None;
                SkipBytes(6);
                GetUInt16();
                var packetType = (CTSPacketType)GetByte();
                IncomingPacket = packetType;
                return packetType;

            case false:
                if (Buffer.Length <6) return CTSPacketType.None;
                IncomingPacket = (CTSPacketType)Buffer[6];
                return IncomingPacket;

            default:
                throw new ArgumentException("Unexpected value for isAuthenticated");
        }
    }

    public ushort GetUInt16()
    {
        return Convert(BitConverter.ToUInt16);
    }

    public uint GetUInt32()
    {
        return Convert(BitConverter.ToUInt32);
    }

    public void SkipBytes(int length)
    {
        if (length + BytesRead > Buffer.Length)
            throw new ArgumentOutOfRangeException("Cannot skip bytes that exceeds the buffer length");
        IncreaseByteRead(length);
    }

    public byte GetByte()
    {
        return Convert((_, _) => Buffer[BytesRead]);
    }

    public ReadOnlySpan<byte> GetBytes(int length)
    {
        var to = BytesRead + length;
        
        if (to > Buffer.Length)
        {
            length = Buffer.Length - BytesRead;
        }

        var result = Buffer.AsSpan(BytesRead, length);
        IncreaseByteRead(length);
        return result;
    }

    /// <summary>
    ///     Get string value based on payload length
    /// </summary>
    /// <returns></returns>
    public string GetString()
    {
        var length = GetUInt16();
        if (length == 0 || BytesRead + length > Buffer.Length) return null;

        var span = GetBytes(length);
        return Iso88591Encoding.GetString(span);
    }

    public void Resize(int length)
    {
        if (length < 0) return;

        Length = length;
        BytesRead = 0;
    }

    public void Reset()
    {
        Buffer = new byte[16394];
        BytesRead = 0;
        Length = 0;
    }

    //public Location GetLocation()
    //{
    //    return new Location { X = GetUInt16(), Y = GetUInt16(), Z = GetByte() };
    //}

    private void IncreaseByteRead(int length)
    {
        BytesRead += length;
    }

    private static int SizeOf<T>() where T : struct
    {
        return Marshal.SizeOf(default(T));
    }

    private T Convert<T>(Func<byte[], int, T> converter) where T : struct
    {
        var result = converter(Buffer, BytesRead);
        IncreaseByteRead(SizeOf<T>());
        return result;
    }
}