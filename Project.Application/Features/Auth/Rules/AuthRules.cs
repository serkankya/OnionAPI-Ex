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
	}
}
