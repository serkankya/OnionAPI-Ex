﻿using FluentValidation;

namespace Project.Application.Features.Auth.Commands.Register
{
	public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
	{
		public RegisterCommandValidator()
		{
			RuleFor(x => x.FullName)
				.NotEmpty()
				.MaximumLength(50)
				.MinimumLength(2)
				.WithName("Ad - Soyad");

			RuleFor(x => x.Email)
				.NotEmpty()
				.MaximumLength(60)
				.EmailAddress()
				.MinimumLength(8)
				.WithName("Email");

			RuleFor(x => x.Password)
				.NotEmpty()
				.MinimumLength(6)
				.WithName("Şifre");

			RuleFor(x => x.ConfirmPassword)
				.NotEmpty()
				.MinimumLength(6)
				.Equal(x => x.Password)
				.WithName("Şifre Tekrarı");
		}
	}
}
