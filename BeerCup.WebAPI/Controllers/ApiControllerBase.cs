using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BeerCup.WebAPI.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        public readonly IMediator mediator;

        public ApiControllerBase(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected async Task<IActionResult> HandleRequest<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>
            where TResponse : ErrorResponseBase
        {
            if (!this.ModelState.IsValid)
            {
                var reasons = this.ModelState
                                .Where(x => x.Value.Errors.Any())
                                .SelectMany(x => x.Value.Errors.Select(x => x.ErrorMessage));

                var errorMessage = String.Join(' ', reasons);

                var errorResponse = new ResponseBase<TResponse>
                {
                    Error = new ErrorModel(errorMessage)
                };

                return BadRequest(errorResponse);
            }

            var userName = GetUserFromClaims();
            if (userName != null && request is RequestBase)
            {
                (request as RequestBase).RequestUsername = userName.ToString();
            }

            var response = await this.mediator.Send(request);
            if (response.Error != null)
            {
                return this.ErrorResponse(response.Error);
            }

            return Ok(response);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetUserFromClaims()
        {
            return this.User?.FindFirstValue(ClaimTypes.Name);
        }

        private IActionResult ErrorResponse(ErrorModel errorModel)
        {
            var httpCode = GetHttpStatusCode(errorModel.Error);
            return StatusCode((int)httpCode, errorModel);
        }

        private static HttpStatusCode GetHttpStatusCode(string errorType)
        {
            switch (errorType)
            {
                case ErrorType.NotFound:
                    return HttpStatusCode.NotFound;
                case ErrorType.InternalServerError:
                    return HttpStatusCode.InternalServerError;
                case ErrorType.Unauthorized:
                    return HttpStatusCode.Unauthorized;
                case ErrorType.RequestTooLarge:
                    return HttpStatusCode.RequestEntityTooLarge;
                case ErrorType.UnsupportedMediaType:
                    return HttpStatusCode.UnsupportedMediaType;
                case ErrorType.UnsupportedMethod:
                    return HttpStatusCode.MethodNotAllowed;
                case ErrorType.TooManyRequests:
                    return (HttpStatusCode)429;
                default:
                    return HttpStatusCode.BadRequest;
            }
        }
    }
}
