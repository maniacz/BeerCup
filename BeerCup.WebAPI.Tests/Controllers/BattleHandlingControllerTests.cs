using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
using BeerCup.ApplicationServices.Components.VotingMachine;
using BeerCup.WebAPI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BeerCup.WebAPI.Tests.Controllers
{
    public class BattleHandlingControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<BattleHandlingController>> _loggerMock;
        private readonly Mock<IVotingMachine> _votingMachineMock;
        private readonly BattleHandlingController _controller;

        public BattleHandlingControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<BattleHandlingController>>();
            _votingMachineMock = new Mock<IVotingMachine>();
            _controller = new BattleHandlingController(_mediatorMock.Object, _loggerMock.Object, _votingMachineMock.Object);
        }

        [Fact]
        public async Task StartBattle_WhenProvidingProperStartBattleRequest_ShouldReturnOk()
        {
            // Arrange
            var request = new StartBattleRequest()
            {
                Place = new BattlePlace()
                {
                    Latitude = 0,
                    Longitude = 0
                }
            };

            _mediatorMock.Setup(x => x.Send(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new StartBattleResponse { Data = Mock.Of<Battle>() });

            // Act
            var result = await _controller.StartBattle(request);

            // Assert
            Assert.NotNull(result);
            var okRequestResult = Assert.IsType<OkObjectResult>(result);
        }
    }
}
