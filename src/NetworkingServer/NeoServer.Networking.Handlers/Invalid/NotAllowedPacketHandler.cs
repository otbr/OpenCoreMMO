using System;
using NeoServer.Networking.Shared.Connection;
using NeoServer.Networking.Shared.Enums;
using NeoServer.Networking.Shared.Messages;
using Serilog;

namespace NeoServer.Networking.Handlers.Invalid;

public class NotAllowedPacketHandler : PacketHandler
{
    private readonly ILogger _logger;
    private readonly CTSPacketType _packet;

    public NotAllowedPacketHandler(CTSPacketType packet, ILogger logger)
    {
        _packet = packet;
        _logger = logger;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var enumText = Enum.GetName(typeof(CTSPacketType), _packet);

        enumText = string.IsNullOrWhiteSpace(enumText) ? _packet.ToString("x") : enumText;
        _logger.Error("Incoming Packet not allowed: {Packet}", enumText);
    }
}