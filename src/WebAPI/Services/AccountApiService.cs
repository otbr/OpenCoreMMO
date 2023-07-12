using AutoMapper;
using NeoServer.Data.Entities;
using NeoServer.Data.Interfaces;
using NeoServer.Web.API.Services.Interfaces;
using NeoServer.Web.Shared.Exceptions;
using NeoServer.Web.Shared.ViewModels.Request;
using NeoServer.Web.Shared.ViewModels.Response;

namespace NeoServer.Web.API.Services;

public class AccountApiService : BaseApiService, IAccountApiService
{
    #region private members

    private readonly IAccountRepository _accountRepository;

    #endregion

    #region constructors

    public AccountApiService(
        IMapper mapper, 
        IAccountRepository accountRepository) : base(mapper)
    {
        _accountRepository = accountRepository;
    }

    #endregion constructors

    #region public methods implementations

    public async Task<IEnumerable<AccountResponseViewModel>> GetAll()
    {
        var players = await _accountRepository.GetAllAsync();
        var response = Mapper.Map<IEnumerable<AccountResponseViewModel>>(players);
        return response;
    }

    public async Task<AccountResponseViewModel> GetById(int playerId)
    {
        var player = await _accountRepository.GetAsync(playerId);
        var response = Mapper.Map<AccountResponseViewModel>(player);
        return response;
    }

    public async Task<AccountResponseViewModel> Create(AccountRequestViewModel request)
    {
        var existentAccount = await _accountRepository.FindByAsync(c => c.EmailAddress == request.EmailAddress);

        if (existentAccount != null)
            throw new NeoConflictException("Account with same Email already registered!");

        var account = Mapper.Map<AccountEntity>(request);

        await _accountRepository.Insert(account);

        return Mapper.Map<AccountResponseViewModel>(account);
    }

    #endregion
}