using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.Services.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MobileApp.Tests
{
    public class BattleDataServiceTests : IDisposable
    {
        private Mock<IGenericRepository> _mockGenericRepository;
        private Mock<ISettingsService> _mockSettingsService;
        private BattleDataService _sut;

        public BattleDataServiceTests()
        {
            Initialize();
        }

        private void Initialize()
        {
            _mockGenericRepository = new Mock<IGenericRepository>();
            _mockSettingsService = new Mock<ISettingsService>();
            _sut = new BattleDataService(_mockGenericRepository.Object, _mockSettingsService.Object);
        }

        [Fact]
        public async Task GetCurrentRunningBattle_ReturnsBattle()
        {
            //Arrange
            _mockGenericRepository.Setup(x => x.GetAsync<ApiResponse<Battle>>(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => new ApiResponse<Battle> { Data = new Battle() });

            //Act
            var currentRunningBattle = await _sut.GetCurrentRunningBattle();

            //Assert
            Assert.IsType<Battle>(currentRunningBattle);
        }

        [Fact]
        public async Task GetBattleResults_ReturnsFiveResults()
        {
            //Arrange
            _mockGenericRepository.Setup(x => x.GetAsync<ApiResponse<List<Result>>>(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => new ApiResponse<List<Result>> { Data = new List<Result> {
                    new Result(1, "BroGar", 30, 60.0M),
                    new Result(2, "Venom", 10, 20.0M),
                    new Result(3, "Kazamat", 5, 10.0M),
                    new Result(4, "Hołda", 3, 6.0M),
                    new Result(5, "Bastion", 2, 4.0M)
                } });

            //Act
            var results = await _sut.GetBattleResults(1);

            //Assert
            Assert.Equal(5, results.Count);
        }

        [Fact]
        public async Task GetTodaysBattle_ReturnsBattleWhenBattleIsScheduledForToday()
        {
            //Arrange
            _mockGenericRepository.Setup(x => x.GetAsync<ApiResponse<Battle>>(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => new ApiResponse<Battle> { Data = new Battle() });

            //Act
            var response = await _sut.GetTodaysBattle();

            //Assert
            Assert.IsType<Battle>(response);
        }

        [Fact]
        public async Task GetFinishedBattles_ReturnsListOfBattles()
        {
            //Arrange
            _mockGenericRepository.Setup(x => x.GetAsync<ApiResponse<List<Battle>>>(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => new ApiResponse<List<Battle>> { Data = new List<Battle>() });

            //Act
            var response = await _sut.GetFinishedBattles();

            //Assert
            Assert.IsType<List<Battle>>(response);
        }

        [Fact]
        public async Task GetBattleUserVotes_ReturnsListOfVote()
        {
            //Arrange
            var battleId = 1;
            _mockGenericRepository.Setup(x => x.GetAsync<ApiResponse<List<Vote>>>(It.Is<string>(s => s.Contains(battleId.ToString())), It.IsAny<string>()))
                .ReturnsAsync(() => new ApiResponse<List<Vote>> { Data = new List<Vote>() });

            //Act
            var response = await _sut.GetBattleUserVotes(battleId);

            //Assert
            Assert.IsType<List<Vote>>(response);
        }

        [Fact]
        public async Task GetAllBattles_ReturnsListOfBattles()
        {
            //Arrange
            _mockGenericRepository.Setup(x => x.GetAsync<ApiResponse<List<Battle>>>(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => new ApiResponse<List<Battle>> { Data = new List<Battle>() });

            //Act
            var response = await _sut.GetAllBattles();

            //Assert
            Assert.IsType<List<Battle>>(response);
        }


        public void Dispose()
        {
        }
    }
}
