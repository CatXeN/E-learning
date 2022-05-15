using MediatR;

namespace E_learningAPI.Application.Commands
{
    public class CreateUserCommand : IRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
