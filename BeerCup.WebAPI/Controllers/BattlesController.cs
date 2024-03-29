﻿using BeerCup.ApplicationServices.API.Domain;
using BeerCup.DataAccess;
using BeerCup.DataAccess.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    public class BattlesController : ApiControllerBase
    {
        public BattlesController(IMediator mediator, ILogger<BattlesController> logger) : base(mediator)
        {
            logger.LogInformation("We are in BattleController");
        }

        [Authorize]
        [HttpGet]
        [Route("")]
        public Task<IActionResult> GetAllBattles([FromQuery] GetBattlesRequest request)
        {
            return this.HandleRequest<GetBattlesRequest, GetBattlesResponse>(request);
        }

        [HttpGet]
        [Route("current")]
        public Task<IActionResult> GetCurrentRunningBattle()
        {
            var request = new GetCurrentRunningBattleRequest();
            return this.HandleRequest<GetCurrentRunningBattleRequest, GetCurrentRunningBattleResponse>(request);
        }

        [HttpGet]
        [Route("TodaysBattle")]
        public Task<IActionResult> GetTodaysBattle()
        {
            var request = new GetTodaysBattleRequest();
            return this.HandleRequest<GetTodaysBattleRequest, GetTodaysBattleResponse>(request);
        }

        [HttpGet]
        [Route("Finished")]
        public Task<IActionResult> GetFinishedBattles()
        {
            var request = new GetFinishedBattlesResultsRequest();
            return this.HandleRequest<GetFinishedBattlesResultsRequest, GetFinishedBattlesResponse>(request);
        }

        [HttpGet]
        [Route("{battleNo}")]
        public Task<IActionResult> GetBattleById([FromRoute] int battleNo)
        {
            var request = new GetBattleByNoRequest()
            {
                BattleId = battleNo
            };

            return this.HandleRequest<GetBattleByNoRequest, GetBattleByNoResponse>(request);
        }

        [HttpPost]
        public Task<IActionResult> SendVotes([FromBody] SendVoteRequest request)
        {
            return this.HandleRequest<SendVoteRequest, SendVotesResponse>(request);
        }

        //[HttpDelete]
        //[Route("UserVotes")]
        //public Task<IActionResult> DeleteUserVotesInCurrentBattle([FromBody] RemoveUserVotesRequest request)
        //{
        //    return this.HandleRequest<RemoveUserVotesRequest, RemoveUserVotesResponse>(request);
        //}

        [HttpDelete]
        [Route("{battleId:int}/{userId:int}")]
        public Task<IActionResult> DeleteUserVotesInCurrentBattle(int battleId, int userId)
        {
            var request = new RemoveUserVotesRequest
            {
                BattleId = battleId,
                UserId = userId
            };

            return this.HandleRequest<RemoveUserVotesRequest, RemoveUserVotesResponse>(request);
        }

        [HttpGet]
        [Route("{battleId:int}/{userId:int}")]
        public Task<IActionResult> GetUserVotesInCurrentBattle(int battleId, int userId)
        {
            var request = new GetUserVotesRequest
            {
                UserId = userId,
                BattleId = battleId
            };

            return this.HandleRequest<GetUserVotesRequest, GetUserVotesResponse>(request);
        }

        [HttpGet]
        [Route("firstRound")]
        public Task<IActionResult> GetFirstRoundBattles()
        {
            var request = new GetFirstRoundBattlesRequest();
            return this.HandleRequest<GetFirstRoundBattlesRequest, GetFirstRoundBattlesResponse>(request);
        }
    }
}
