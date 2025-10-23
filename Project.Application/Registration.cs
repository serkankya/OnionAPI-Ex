using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Behaviours;
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

			services.AddValidatorsFromAssembly(assembly);

			ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("tr");

			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehaviour<,>));
		}
	}
}
