using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.Services.Data;
using Moq;
using System;
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
            mockGenericRepository.Setup(x => x.GetAsync<ApiResponse<Battle>>(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(() => new ApiResponse<Battle> { Data = new Battle() });
            var sut = new BattleDataService(mockGenericRepository.Object, mockSettingsService.Object);

            //Act
            var currentRunningBattle = await sut.GetCurrentRunningBattle();

            //Assert
            Assert.IsType<Battle>(currentRunningBattle);
        }
    }
}
