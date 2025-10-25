using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Infrastructure.Tokens;

namespace Project.Infrastructure
{
	public static class Registration
	{
		public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
		{
			services.Configure<TokenSettings>(config.GetSection("JWT"));
		}
	}
}
