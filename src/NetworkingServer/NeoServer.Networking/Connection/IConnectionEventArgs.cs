namespace NeoServer.Networking.Connection;

public interface IConnectionEventArgs
{
    IConnection Connection { get; }
}