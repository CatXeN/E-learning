using E_learningAPI.Application.Queries;
using E_learningAPI.Domain.Config;
using E_learningAPI.Domain.Data;
using E_learningAPI.Domain.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_learningAPI.Application.QueriesHandlers
{
    public class GetAuthorizeQueryHandler : IRequestHandler<GetAuthorizeQuery, string>
    {

        private readonly DataContext _context;
        private readonly TokenConfig _config;

        public GetAuthorizeQueryHandler(DataContext context, IOptions<TokenConfig> config)
        {
            _context = context;
            _config = config.Value;
        }

        public async Task<string> Handle(GetAuthorizeQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username);

            if (user == null)
                throw new Exception("Username or password is wrong");

            if (!EncryptHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Username or password is wrong");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(24),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
