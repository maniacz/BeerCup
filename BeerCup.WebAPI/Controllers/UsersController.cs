using BeerCup.ApplicationServices.API.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ApiControllerBase
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("")]
        public Task<IActionResult> GetAll([FromQuery] GetUserRequest request)
        {
            return this.HandleRequest<GetUserRequest, GetUserResponse>(request);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            return this.HandleRequest<CreateUserRequest, CreateUserResponse>(request);
        }

        [HttpGet]
        [Route("me")]
        public Task<IActionResult> GetMe()
        {
            //var user = this.GetUserFromClaims();
            //if (user != null)
            //{
            //    return Task.FromResult(Ok() as IActionResult);
            //}
            //else
            //{
            //    return Task.FromResult(Unauthorized() as IActionResult);
            //}
            return Task.FromResult(Ok() as IActionResult);
        }

        [HttpGet]
        [Route("authenticate")]
        public Task<IActionResult> AuthenticateUser()
        {
            var basicAuthenticationCredentials = GetMyCredentialsFromBasicAuthentication();
            var request = new AuthenticationRequest()
            {
                Username = basicAuthenticationCredentials["username"],
                Password = basicAuthenticationCredentials["password"]
            };
            return this.HandleRequest<AuthenticationRequest, AuthenticationResponse>(request);
        }

        //todo: Czy to nie powinno być przeniesione do innej klasy/folderu?
        private Dictionary<string, string> GetMyCredentialsFromBasicAuthentication()
        {
            var credentials = new Dictionary<string, string>();
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentialsArray = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            credentials.Add("username", credentialsArray[0]);
            credentials.Add("password", credentialsArray[1]);

            return credentials;
        }
    }
}
