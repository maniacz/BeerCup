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
    public class BeerController : ControllerBase
    {
        private readonly IMediator mediator;

        public BeerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddBeer([FromBody] AddBeerRequest request)
        {
            var response = await this.mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("beerId")]
        public async Task<IActionResult> RemoveBeer([FromRoute] int beerId)
        {
            var request = new RemoveBeerRequest()
            {
                BeerId = beerId
            };

            var response = await this.mediator.Send(request);
            return Ok(response);
        }
    }
}
