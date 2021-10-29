using BeerCup.ApplicationServices.API.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerCup.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattleHandlingController : ApiControllerBase
    {
        public BattleHandlingController(IMediator mediator, ILogger<BattleHandlingController> logger) : base(mediator)
        {

        }

        [HttpPut]
        [Route("")]
        public Task<IActionResult> StartBattle(StartBattleRequest request)
        {
            //var request = new StartBattleRequest();
            return this.HandleRequest<StartBattleRequest, StartBattleResponse>(request);
        }

        [HttpPut]
        [Route("EndBattle")]
        public Task<IActionResult> EndBattle(EndBattleRequest request)
        {
            return this.HandleRequest<EndBattleRequest, EndBattleResponse>(request);
        }

        [HttpGet]
        [Route("ShowResults/{battleId}")]
        public Task<IActionResult> ShowBattleResults(int battleId)
        {
            var request = new ShowBattleResultRequest
            {
                BattleId = battleId
            };
            return this.HandleRequest<ShowBattleResultRequest, ShowBattleResultResponse>(request);
        }

        [HttpPut]
        [Route("PublishResults")]
        public Task<IActionResult> PublishBattleResults(PublishResultsRequest request)
        {
            return this.HandleRequest<PublishResultsRequest, PublishResultsResponse>(request);
        }

        /*
        [HttpPost]
        [Route("place")]
        public Task<IActionResult> AddBattlePlace([FromBody] AddBattlePlaceRequest request)
        {
            return this.HandleRequest<AddBattlePlaceRequest, AddBattlePlaceResponse>(request);
        }
        */
    }


}
