namespace NeoServer.Web.Shared.ViewModels.Request;

public class AccountRequestViewModel : IViewModel
{
    public string Password { get; set; }
    public string EmailAddress { get; set; }
    public int PremiumDays { get; set; }
}