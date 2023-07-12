using AutoMapper;
using NeoServer.Data.Entities;
using NeoServer.Web.Shared.ViewModels.Request;
using NeoServer.Web.Shared.ViewModels.Response;

namespace NeoServer.Web.API.AutoMapperProfiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountEntity, AccountResponseViewModel>();
        CreateMap<AccountResponseViewModel, AccountEntity>();

        CreateMap<AccountEntity, AccountRequestViewModel>();
        CreateMap<AccountRequestViewModel, AccountEntity>();
    }
}