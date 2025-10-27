
using Project.Application;
using Project.Persistence;
using Project.Mapper;
using Project.Application.Exceptions;
using Project.Infrastructure;
using Microsoft.OpenApi.Models;

namespace Project.WebAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddSwaggerGen(s =>
			{
				s.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "API Proejct",
					Version = "v1",
					Description = "Onion Architecture & Clean Architecture - JWT Auth Example"
				});
				s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "Enter 'Bearer' followed by your JWT token. Example: **Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...**"
				});
				s.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
				}
				});
			});

			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
			builder.Services.AddOpenApi();

			var env = builder.Environment;

			builder.Configuration
				.SetBasePath(env.ContentRootPath)
						.AddJsonFile("appsettings.json", optional: false)
						.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			builder.Services.AddPersistence(builder.Configuration);
			builder.Services.AddInfrastructure(builder.Configuration);
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
