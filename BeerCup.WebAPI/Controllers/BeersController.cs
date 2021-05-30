using BeerCup.ApplicationServices.API.Domain;
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
    public class BeersController : ControllerBase
    {
        private readonly IMediator mediator;

        public BeersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllBeers([FromQuery] GetBeersRequest request)
        {
            var response = await this.mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [Route("{beerId}")]
        public async Task<IActionResult> GetBeerById([FromRoute] int beerId)
        {
            var request = new GetBeerByIdRequest()
            {
                BeerId = beerId
            };

            var response = await this.mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddBeer([FromBody] AddBeerRequest request)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest("BAD_REQUEST_1234");
            }

            var response = await this.mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> AlterBeer([FromBody] AlterBeerRequest  request)
        {
            var response = await this.mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{beerId}")]
        public async Task<IActionResult> RemoveBeer([FromRoute] int beerId)
        {
            //todo: Zmodyfikować tak, żeby zwracało 404 jak nie znajdzie piwa o takim id
            var request = new RemoveBeerRequest()
            {
                BeerId = beerId
            };

            var response = await this.mediator.Send(request);
            return Ok(response);
        }
    }
}
