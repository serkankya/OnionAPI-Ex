﻿using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;
using System.ComponentModel.DataAnnotations;

namespace Project.Application.Exceptions
{
	public class ExceptionMiddleware : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			int statusCode = GetStatusCode(exception);
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = statusCode;

			List<string> errors = new()
			{
			$"Bir hata oluştu. Hata mesajı : {exception.Message}",
			$"Hata detayları : {exception.InnerException?.ToString()}"
			};

			return context.Response.WriteAsync(new ExceptionModel
			{
				Errors = errors,
				StatusCode = statusCode
			}.ToString());
		}

		private static int GetStatusCode(Exception exception) =>
			exception switch
			{
				BadRequestException => StatusCodes.Status400BadRequest,
				NotFoundException => StatusCodes.Status404NotFound,
				ValidationException => StatusCodes.Status422UnprocessableEntity,
				_ => StatusCodes.Status500InternalServerError
			};
	}
}
