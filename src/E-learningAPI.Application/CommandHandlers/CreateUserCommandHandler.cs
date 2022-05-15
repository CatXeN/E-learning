using E_learningAPI.Application.Commands;
using E_learningAPI.Domain.Data;
using E_learningAPI.Domain.Entities;
using E_learningAPI.Domain.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningAPI.Application.CommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private DataContext _context { get; set; }

        public CreateUserCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Users.AnyAsync(x => x.Username == request.Username))
            {
                throw new Exception("This username is already taken.");
            }

            byte[] hash, salt;
            EncryptHelper.CreatePasswordHashAndSalt(request.Password, out hash, out salt);

            var user = new User() { Username = request.Username, Email = request.Email };

            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
