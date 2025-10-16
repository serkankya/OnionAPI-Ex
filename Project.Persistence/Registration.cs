using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Persistence.Context;

namespace Project.Persistence
{
	public static class Registration
	{
		public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(opt =>
			{
				opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

				opt.ConfigureWarnings(warnings =>
					warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
			});
		}
	}
}
