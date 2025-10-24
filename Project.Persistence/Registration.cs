using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Interfaces.Repositories;
using Project.Application.Interfaces.UnitOfWorks;
using Project.Domain.Entities;
using Project.Persistence.Context;
using Project.Persistence.Repositories;
using Project.Persistence.UnitOfWorks;

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

			services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
			services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddIdentityCore<User>(opt =>
			{
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequiredLength = 2;
				opt.Password.RequireLowercase = false;
				opt.Password.RequireUppercase = false;
				opt.Password.RequireDigit = false;
				opt.SignIn.RequireConfirmedEmail = false;
			})
				.AddRoles<Role>().AddEntityFrameworkStores<AppDbContext>();
		}
	}
}
