using System;
using NeoServer.Server.Common.Contracts.Network;
using NeoServer.Server.Common.Contracts.Network.Enums;
using Serilog;

namespace NeoServer.Networking.Handlers;

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