using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Project.Application.Interfaces.Tokens;
using Project.Infrastructure.Tokens;
using System.Text;

namespace Project.Infrastructure
{
	public static class Registration
	{
		public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
		{
			services.Configure<TokenSettings>(config.GetSection("JWT"));

			services.AddTransient<ITokenService, TokenService>();

			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
			{
				opt.SaveToken = true;
				opt.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"])),
					ValidateLifetime = false,
					ValidIssuer = config["JWT:Issuer"],
					ValidAudience = config["JWT:Audience"],
					ClockSkew = TimeSpan.Zero
				};
			});
		}
	}
}
