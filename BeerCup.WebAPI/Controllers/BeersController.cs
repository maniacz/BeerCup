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
    public class BeersController : ApiControllerBase
    {
        public BeersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("")]
        public Task<IActionResult> GetAllBeers([FromQuery] GetBeersRequest request)
        {
            return this.HandleRequest<GetBeersRequest, GetBeersResponse>(request);
        }

        [HttpGet]
        [Route("{beerId}")]
        public Task<IActionResult> GetBeerById([FromRoute] int beerId)
        {
            var request = new GetBeerByIdRequest()
            {
                BeerId = beerId
            };

            return this.HandleRequest<GetBeerByIdRequest, GetBeerByIdResponse>(request);
        }

        [HttpPost]
        [Route("")]
        public Task<IActionResult> AddBeer([FromBody] AddBeerRequest request)
        {
            return this.HandleRequest<AddBeerRequest, AddBeerResponse>(request);
        }

        [HttpPut]
        [Route("")]
        public Task<IActionResult> AlterBeer([FromBody] AlterBeerRequest  request)
        {
            return this.HandleRequest<AlterBeerRequest, AlterBeerResponse>(request);
        }

        [HttpDelete]
        [Route("{beerId}")]
        public Task<IActionResult> RemoveBeer([FromRoute] int beerId)
        {
            //todo: Zmodyfikować tak, żeby zwracało 404 jak nie znajdzie piwa o takim id
            var request = new RemoveBeerRequest()
            {
                BeerId = beerId
            };

            return this.HandleRequest<RemoveBeerRequest, RemoveBeerResponse>(request);
        }
    }
}
