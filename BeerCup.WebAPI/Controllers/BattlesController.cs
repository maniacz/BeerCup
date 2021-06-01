using BeerCup.ApplicationServices.API.Domain;
using BeerCup.DataAccess;
using BeerCup.DataAccess.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public BattlesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("")]
        public Task<IActionResult> GetAllBattles([FromQuery] GetBattlesRequest request)
        {
            return this.HandleRequest<GetBattlesRequest, GetBattlesResponse>(request);
        }

        [HttpGet]
        [Route("{battleId}")]
        public Task<IActionResult> GetBattleById([FromRoute] int battleId)
        {
            var request = new GetBattleByIdRequest()
            {
                BattleId = battleId
            };

            return this.HandleRequest<GetBattleByIdRequest, GetBattleByIdResponse>(request);
        }
    }
}
