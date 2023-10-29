using System;
using Serilog;
using NeoServer.Networking.Enums;
using NeoServer.Networking.Messages;
using NeoServer.Networking.Connection;

namespace NeoServer.Networking.Handlers.Invalid;

public class NotImplementedPacketHandler : PacketHandler
{
    private readonly ILogger _logger;
    private readonly CTSPacketType _packet;

    public NotImplementedPacketHandler(CTSPacketType packet, ILogger logger)
    {
        _packet = packet;
        _logger = logger;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var enumText = Enum.GetName(typeof(CTSPacketType), _packet);

        enumText = string.IsNullOrWhiteSpace(enumText) ? _packet.ToString("x") : enumText;
        _logger.Error("Incoming Packet not handled: {Packet}", enumText);
    }
}