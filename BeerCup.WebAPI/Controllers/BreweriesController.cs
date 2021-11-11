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
    public class BreweriesController : ApiControllerBase
    {
        public BreweriesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("")]
        public Task<IActionResult> AddBrewery([FromBody] AddBreweryRequest request)
        {
            return this.HandleRequest<AddBreweryRequest, AddBreweryResponse>(request);
        }

        [HttpGet]
        [Route("")]
        public Task<IActionResult> GetAllBreweries([FromRoute] GetBreweriesRequest request)
        {
            return this.HandleRequest<GetBreweriesRequest, GetBreweriesResponse>(request);
        }

        [HttpPut]
        [Route("")]
        public Task<IActionResult> EditBreweryName([FromBody] EditBreweryNameRequest request)
        {
            return this.HandleRequest<EditBreweryNameRequest, EditBreweryNameResponse>(request);
        }

        [HttpDelete]
        [Route("{breweryId}")]
        public Task<IActionResult> DeleteBrewery([FromRoute] int breweryId)
        {
            var request = new RemoveBreweryRequest
            {
                breweryId = breweryId
            };

            return this.HandleRequest<RemoveBreweryRequest, RemoveBreweryResponse>(request);
        }

        //[HttpGet]
        //[Route("")]
        //public Task<IActionResult> GetBreweryByBeerId([FromQuery] GetBreweryByBeerIdRequest request)
        //{
        //    return this.HandleRequest<GetBreweryByBeerIdRequest, GetBreweryByBeerIdResponse>(request);
        //}
    }
}
