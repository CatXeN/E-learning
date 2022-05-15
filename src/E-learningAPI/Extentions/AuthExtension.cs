using E_learningAPI.Domain.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_learningAPI.Extentions
{
    public static class AuthExtension
    {
        public static void AddAuthExtension(this IServiceCollection services, ConfigurationManager Configuration)
        {
            services.Configure<TokenConfig>(options => Configuration.GetSection("Token").Bind(options));

            var tokenConfigurationSection = Configuration.GetSection("Token");
            var tokenConfig = tokenConfigurationSection.Get<TokenConfig>();
            var key = Encoding.ASCII.GetBytes(tokenConfig.SecurityKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
    }
}
