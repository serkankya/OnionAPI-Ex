using Microsoft.AspNetCore.Builder;

namespace Project.Application.Exceptions
{
	public static class ConfigureExceptionMiddlware
	{
		public static void ConfigureExceptionHandlingMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionMiddleware>();
		}
	}
}
