using System;

namespace NeoServer.Networking.Messages;

public interface INetworkMessage : IReadOnlyNetworkMessage
{
    void AddByte(byte b);
    void AddBytes(ReadOnlySpan<byte> bytes);
    void AddPaddingBytes(int count);
    void AddString(string value);
    void AddUInt16(ushort value);
    void AddUInt32(uint value);
    byte[] AddHeader(bool addChecksum = true);
    void AddLocation(dynamic location);
    void AddLength();
}