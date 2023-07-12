using NeoServer.Web.Shared.ViewModels.Request;
using NeoServer.Web.Shared.ViewModels.Response;

namespace NeoServer.Web.API.Services.Interfaces;

public interface IAccountApiService
{
    Task<IEnumerable<AccountResponseViewModel>> GetAll();
    Task<AccountResponseViewModel> GetById(int accountId);
    Task<AccountResponseViewModel> Create(AccountRequestViewModel request);
}