using NeoServer.Game.Common.Creatures.Players;

namespace NeoServer.Web.Shared.ViewModels.Request;

public class PlayerRequestViewModel : IViewModel
{
    public string Name { get; set; }
    public Gender Sex { get; set; }
    public byte Vocation { get; set; }
    public int Town { get; set; }
    public int AccountId { get; set; }
    public int WorldId { get; set; }
    public int PosX { get; set; }
    public int PosY { get; set; }
    public int PosZ { get; set; }
}