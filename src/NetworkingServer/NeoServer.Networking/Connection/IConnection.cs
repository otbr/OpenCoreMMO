using System;
using System.Collections.Generic;
using NeoServer.Networking.Messages;

namespace NeoServer.Networking.Connection;

public interface IConnection
{
    IReadOnlyNetworkMessage InMessage { get; }
    uint[] XteaKey { get; }
    uint CreatureId { get; }
    bool IsAuthenticated { get; }
    bool Disconnected { get; }
    Queue<IOutgoingPacket> OutgoingPackets { get; }
    long LastPingRequest { get; set; }
    long LastPingResponse { get; set; }
    string Ip { get; }

    event EventHandler<IConnectionEventArgs> OnProcessEvent;
    event EventHandler<IConnectionEventArgs> OnCloseEvent;
    event EventHandler<IConnectionEventArgs> OnPostProcessEvent;

    void BeginStreamRead();
    void Close(bool force = false);
    void Disconnect(string text = null);
    void Send(IOutgoingPacket packet);
    void SendFirstConnection();
    void SetXtea(uint[] xtea);
    void SetAsAuthenticated();
    void SetConnectionOwner(uint creatureId);
    void Send();
}