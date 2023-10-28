using System;

namespace NeoServer.Networking.Shared.Connection;

public class ConnectionEventArgs : EventArgs, IConnectionEventArgs
{
    public ConnectionEventArgs(Connection connection)
    {
        Connection = connection;
    }

    public IConnection Connection { get; }
}