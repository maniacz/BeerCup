using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Handlers;
using BeerCup.ApplicationServices.Mappings;
using BeerCup.DataAccess;
using BeerCup.DataAccess.CQRS.Commands;
using BeerCup.DataAccess.CQRS.Queries;
using BeerCup.DataAccess.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ApiModel = BeerCup.ApplicationServices.API.Domain.Models;

namespace BeerCup.WebAPI.Tests.API
{
    public class HandlersTests
    {
        private readonly Mock<ICommandExecutor> _commandExecutorMock;
        private readonly Mock<IQueryExecutor> _queryExecutorMock;
        private readonly IMapper _mapper;
        private readonly EndBattleHandler _endBattleHandler;

        public HandlersTests()
        {
            _commandExecutorMock = new Mock<ICommandExecutor>();
            _queryExecutorMock = new Mock<IQueryExecutor>();
            var battlesProfile = new BattlesProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(battlesProfile));
            _mapper = new Mapper(config);
            _endBattleHandler = new EndBattleHandler(_commandExecutorMock.Object, _queryExecutorMock.Object, _mapper);
        }

        [Fact]
        public async Task HandleEndBattle_WhenThereIsNoRunningBattle_ShouldReturnNotFound()
        {
            // Arrange
            var request = new EndBattleRequest
            {
                Id = 0
            };

            _queryExecutorMock.Setup(x => x.Execute(It.IsAny<GetRunningBattleQuery>())).ReturnsAsync(() => null);

            // Act
            var result = await _endBattleHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EndBattleResponse>(result);
            Assert.NotNull(result.Error);
            Assert.Equal("NOT_FOUND", result.Error.Error.ToUpper());
        }

        [Fact]
        public async Task HandleEndBattle_UpdateDbCommandFailed_ShouldReturnInternalServerError()
        {
            // Arrange
            var request = new EndBattleRequest
            {
                Id = 0
            };

            _queryExecutorMock.Setup(x => x.Execute(It.IsAny<GetRunningBattleQuery>())).ReturnsAsync(new Battle());
            _commandExecutorMock.Setup(x => x.Execute(It.IsAny<EndBattleCommand>())).ReturnsAsync(() => null);

            // Act
            var result = await _endBattleHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EndBattleResponse>(result);
            Assert.NotNull(result.Error);
            Assert.Equal("INTERNAL_SERVER_ERROR", result.Error.Error);
        }

        [Fact]
        public async Task HandleEndBattle_UpdateDbOk_ShouldReturnResponseWithBattleApiModel()
        {
            // Arrange
            var request = new EndBattleRequest
            {
                Id = 1
            };

            _queryExecutorMock.Setup(x => x.Execute(It.IsAny<GetRunningBattleQuery>())).ReturnsAsync(new Battle());
            _commandExecutorMock.Setup(x => x.Execute(It.IsAny<EndBattleCommand>())).ReturnsAsync(() => Mock.Of<Battle>());

            // Act
            var result = await _endBattleHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EndBattleResponse>(result);
            Assert.NotNull(result.Data);
            Assert.IsType<ApiModel.Battle>(result.Data);
        }
    }
}
