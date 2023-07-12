using AutoMapper;
using NeoServer.Data.Entities;
using NeoServer.Data.Interfaces;
using NeoServer.Web.API.Services.Interfaces;
using NeoServer.Web.Shared.Exceptions;
using NeoServer.Web.Shared.ViewModels.Request;
using NeoServer.Web.Shared.ViewModels.Response;

namespace NeoServer.Web.API.Services;

public class PlayerApiService : BaseApiService, IPlayerApiService
{
    #region private members

    private readonly IPlayerRepository _playerRepository;
    private readonly IAccountRepository _accountRepository;

    #endregion

    #region constructors

    public PlayerApiService(
        IMapper mapper,
        IPlayerRepository playerRepository,
        IAccountRepository accountRepository) : base(mapper)
    {
        _playerRepository = playerRepository;
        _accountRepository = accountRepository;
    }

    #endregion constructors

    #region public methods implementations

    public async Task<IEnumerable<PlayerResponseViewModel>> GetAll()
    {
        var players = await _playerRepository.GetAllAsync();
        var response = Mapper.Map<IEnumerable<PlayerResponseViewModel>>(players);
        return response;
    }

    public async Task<PlayerResponseViewModel> GetById(int playerId)
    {
        var player = await _playerRepository.GetAsync(playerId);

        if (player == null)
            throw new NeoNotFoundException("Player not found!");

        var response = Mapper.Map<PlayerResponseViewModel>(player);
        return response;
    }

    public async Task<PlayerResponseViewModel> Create(PlayerRequestViewModel request)
    {
        var existentAccount = await _accountRepository.GetAsync(request.AccountId);

        if (existentAccount != null)
            throw new NeoNotFoundException("Account not found!");

        var existentPlayer = await _playerRepository.FindByAsync(c => c.Name == request.Name);

        if (existentPlayer != null)
            throw new NeoConflictException("Player with same Name already registered!");

        var player = Mapper.Map<PlayerEntity>(request);

        await _playerRepository.Insert(player);

        return Mapper.Map<PlayerResponseViewModel>(player);
    }

    #endregion
}