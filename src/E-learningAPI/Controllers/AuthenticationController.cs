using E_learningAPI.Application.Commands;
using E_learningAPI.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_learningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserCommand createUserCommand)
        {
            try
            {
                await _mediator.Send(createUserCommand);
            }
            catch (Exception ex)
            {

                throw;
            }

            return new JsonResult("The account has been created successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(GetAuthorizeQuery getAuthorizeQuery)
        {
            string token;

            try
            {
                token = await _mediator.Send(getAuthorizeQuery);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            return new JsonResult(token);
        }

    }
}
