using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.Components.VotingMachine;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BeerCup.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattleHandlingController : ApiControllerBase
    {
        private readonly IVotingMachine _votingMachine;

        public BattleHandlingController(IMediator mediator, ILogger<BattleHandlingController> logger, IVotingMachine votingMachine) : base(mediator)
        {
            _votingMachine = votingMachine;
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

        [HttpGet]
        [Route("Routing/{battleId}")]
        public Task<IActionResult> GetBattleRouting([FromRoute] int battleId)
        {
            var request = new GetBattleRoutingRequest
            {
                BattleId = battleId
            };

            return this.HandleRequest<GetBattleRoutingRequest, GetBattleRoutingResponse>(request);
        }

        [HttpGet]
        [Route("FirstBattleFromRouting/{nextBattleNo}")]
        public Task<IActionResult> GetFirstBattleFromRouting([FromRoute] int nextBattleNo)
        {
            var request = new GetFirstBattleFromRoutingRequest
            {
                NextBattleNo = nextBattleNo
            };

            return this.HandleRequest<GetFirstBattleFromRoutingRequest, GetFirstBattleFromRoutingResponse>(request);
        }

        [HttpPost]
        [Route("AddNewBattle")]
        public Task<IActionResult> AddNewBattle(AddNewBattleRequest request)
        {
            return this.HandleRequest<AddNewBattleRequest, AddNewBattleResponse>(request);
        }

        [HttpPut]
        [Route("EditBattle")]
        public Task<IActionResult> EditBattle(EditBattleRequest request)
        {
            return this.HandleRequest<EditBattleRequest, EditBattleResponse>(request);
        }

        [HttpPost]
        [Route("AwardDrawing/{battleId}")]
        public Task<IActionResult> SetAwardWinnerFromVoters([FromRoute] int battleId, [FromQuery] bool isPaperVoteWinner = false)
        {
            var request = new SetAwardWinnerRequest
            {
                IsPaperVoteWinner = isPaperVoteWinner,
                BattleId = battleId
            };
            return this.HandleRequest<SetAwardWinnerRequest, SetAwardWinnerResponse>(request);
        }

        [HttpGet]
        [Route("AwardDrawing/{battleId}")]
        public Task<IActionResult> GetLuckyVoter([FromRoute] int battleId)
        {
            var request = new GetLuckyVoterRequest
            {
                battleId = battleId
            };
            return this.HandleRequest<GetLuckyVoterRequest, GetLuckyVoterResponse>(request);
        }

        [HttpPut]
        [Route("AwardDrawing/{battleId}")]
        public Task<IActionResult> EditLuckyVoter([FromBody] EditLuckyVoterRequest request)
        {
            return this.HandleRequest<EditLuckyVoterRequest, EditLuckyVoterResponse>(request);
        }

        [HttpPost]
        [Route("AwardDrawingForAll/{battleId}/{paperVotesCount}")]
        public async Task<IActionResult> SetLuckyVoterIncludingPaperVotes([FromRoute] int battleId, [FromRoute] int paperVotesCount)
        {
            var isPaperVoterWinner = await _votingMachine.IsDrawWinnerFromPaperVotes(paperVotesCount, battleId);
            return await SetAwardWinnerFromVoters(battleId, isPaperVoterWinner);
        }
    }
}
