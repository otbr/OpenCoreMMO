using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NeoServer.Web.Shared.Extensions;
using NeoServer.Web.Shared.ViewModels.Request;
using NeoServer.Web.Shared.ViewModels.Response;
using System.Net;
using Xunit;

namespace NeoServer.WebApi.Tests.Tests;

public class PlayerTests : BaseIntegrationTests
{
    #region Get Tests

    [Fact(DisplayName = "Get All Players")]
    public async Task Get_All_Players()
    {
        // Arrange
        var playersCount = await NeoContext.Players.CountAsync();

        // Act
        var response =
            await NeoHttpClient.GetAndDeserialize<IEnumerable<PlayerResponseViewModel>>("api/Player");

        //Assert
        response.Count().Should().Be(playersCount);
    }

    [Fact(DisplayName = "Get Player By Id")]
    public async Task Get_Player_By_Id()
    {
        // Arrange
        var player = await CreatePlayer();

        // Act
        var response =
            await NeoHttpClient.GetAndDeserialize<PlayerResponseViewModel>($"api/Player/{player.Id}");

        //Assert
        response.Name.Should().Be(player.Name);
    }

    #endregion

    #region Create Tests

    [Fact(DisplayName = "Create Player")]
    public async Task Create_Player()
    {
        // Arrange
        var account = await CreateAccount();

        var player = new PlayerRequestViewModel
        {
            Name = GenerateRandomString(10),
            Town = 1,
            Sex = Game.Common.Creatures.Players.Gender.Male,
            AccountId = account.Id,
        };

        // Act
        var response =
            await NeoHttpClient.PostAndDeserialize<PlayerResponseViewModel>("api/Player", player);

        //Assert
        response.Name.Should().Be(player.Name);
    }

    [Fact(DisplayName = "Create Player Invalid Account")]
    public async Task Create_Player_Invalid_Account()
    {
        // Arrange
        var player = new PlayerRequestViewModel
        {
            Name = GenerateRandomString(10),
            Town = 1,
            Sex = Game.Common.Creatures.Players.Gender.Male,
            AccountId = -1,
        };

        // Act
        var response =
            await NeoHttpClient.PostAsync("api/Player", EncodeEntityData(player));

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact(DisplayName = "Create Player Invalid Account")]
    public async Task Create_Player_Invalid_Name()
    {
        // Arrange
        var account = await CreateAccount();

        var existentPlayer = await CreatePlayer();

        var player = new PlayerRequestViewModel
        {
            Name = existentPlayer.Name,
            Town = 1,
            Sex = Game.Common.Creatures.Players.Gender.Male,
            AccountId = account.Id,
        };

        // Act
        var response =
            await NeoHttpClient.PostAsync("api/Player", EncodeEntityData(player));

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    #endregion
}