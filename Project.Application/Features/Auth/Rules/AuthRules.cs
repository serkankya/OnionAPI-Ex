using Project.Application.Bases;
using Project.Application.Features.Auth.Exceptions;
using Project.Domain.Entities;

namespace Project.Application.Features.Auth.Rules
{
	public class AuthRules : BaseRules
	{
		public Task? UserShouldNotBeExist(User? user)
		{
			if (user is not null)
				throw new UserAlreadyExistsException();

			return Task.CompletedTask;
		}

		public Task? EmailOrPasswordShouldNotBeInvalid(User? user, bool checkPassword)
		{
			if (user is null || checkPassword == false)
			{
				throw new EmailOrPasswordShouldNotBeInvalidException();
			}

			return Task.CompletedTask;
		}
	}
}
