
using Project.Application;
using Project.Persistence;
using Project.Mapper;
using Project.Application.Exceptions;

namespace Project.WebAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddSwaggerGen();
			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
			builder.Services.AddOpenApi();

			var env = builder.Environment;

			builder.Configuration
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			builder.Services.AddPersistence(builder.Configuration);
			builder.Services.AddApplication();
			builder.Services.AddCustomMapper();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.ConfigureExceptionHandlingMiddleware();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
