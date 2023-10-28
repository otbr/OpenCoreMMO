namespace NeoServer.Networking.Shared.Connection;

public interface IConnectionEventArgs
{
    IConnection Connection { get; }
}