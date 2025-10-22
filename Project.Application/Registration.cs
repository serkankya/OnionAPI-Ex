using Microsoft.Extensions.DependencyInjection;
using Project.Application.Exceptions;
using System.Reflection;

namespace Project.Application
{
	public static class Registration
	{
		public static void AddApplication(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();

			services.AddTransient<ExceptionMiddleware>();

			services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
		}
	}
}
