﻿using BeerCup.ApplicationServices.API.Domain;
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
            
            //var response = await this.mediator.Send(request);
            //return Ok(response);
        }

        [HttpGet]
        [Route("{battleId}")]
        public async Task<IActionResult> GetBattleById([FromRoute] int battleId)
        {
            var request = new GetBattleByIdRequest()
            {
                BattleId = battleId
            };

            var response = await this.mediator.Send(request);
            return Ok(response);
        }

        //public IEnumerable<Battle> GetAllBattles() => this.battleRepository.GetAll();

        //[HttpGet]
        //[Route("battleId")]
        //public async Task<IActionResult> GetBattleById(int id)
        //{
        //    var response = await this.mediator.Send(id);
        //    return Ok(response);
        //}

        //public Battle GetBattleById(int battleId) => this.battleRepository.GetById(battleId);
    }
}
