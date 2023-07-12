using Microsoft.AspNetCore.Mvc;
using NeoServer.Web.API.Services.Interfaces;
using NeoServer.Web.Shared.ViewModels.Request;

namespace NeoServer.Web.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : BaseController
{
    #region private members

    private readonly IAccountApiService _accountApiService;

    #endregion

    #region constructor

    public AccountController(IAccountApiService accountApiService)
    {
        _accountApiService = accountApiService;
    }

    #endregion

    #region public methods

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Response(await _accountApiService.GetAll());
    }

    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetById([FromRoute] int accountId)
    {
        return Response(await _accountApiService.GetById(accountId));
    }

    [HttpPost()]
    public async Task<IActionResult> Create(AccountRequestViewModel request)
    {
        var result = await _accountApiService.Create(request);

        if (result != null)
            return Response(result);

        return Ok();
    }

    #endregion
}