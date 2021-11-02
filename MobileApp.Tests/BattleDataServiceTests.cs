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
    public class BattleDataServiceTests
    {
        [Fact]
        public async Task GetCurrentRunningBattle_ReturnsBattle()
        {
            //Arrange
            var mockGenericRepository = new Mock<IGenericRepository>();
            var mockSettingsService = new Mock<ISettingsService>();
            mockGenericRepository.Setup(x => x.GetAsync<ApiResponse<Battle>>(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => new ApiResponse<Battle> { Data = new Battle() });
            var sut = new BattleDataService(mockGenericRepository.Object, mockSettingsService.Object);

            //Act
            var currentRunningBattle = await sut.GetCurrentRunningBattle();

            //Assert
            Assert.IsType<Battle>(currentRunningBattle);
        }

        [Fact]
        public async Task GetBattleResults_ReturnsFiveResults()
        {
            //Arrange
            var mockGenericRepository = new Mock<IGenericRepository>();
            var mockSettingsService = new Mock<ISettingsService>();
            mockGenericRepository.Setup(x => x.GetAsync<ApiResponse<List<Result>>>(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => new ApiResponse<List<Result>> { Data = new List<Result> {
                    new Result(1, "BroGar", 30, 60.0M),
                    new Result(2, "Venom", 10, 20.0M),
                    new Result(3, "Kazamat", 5, 10.0M),
                    new Result(4, "Hołda", 3, 6.0M),
                    new Result(5, "Bastion", 2, 4.0M)
                } });
            var sut = new BattleDataService(mockGenericRepository.Object, mockSettingsService.Object);

            //Act
            var results = await sut.GetBattleResults(1);

            //Assert
            Assert.Equal(5, results.Count);
        }
    }
}
