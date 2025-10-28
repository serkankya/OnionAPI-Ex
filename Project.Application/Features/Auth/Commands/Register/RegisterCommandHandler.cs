using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project.Application.Bases;
using Project.Application.Features.Auth.Rules;
using Project.Application.Interfaces.CustomAutoMapper;
using Project.Application.Interfaces.UnitOfWorks;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Auth.Commands.Register
{
	internal class RegisterCommandHandler : BaseHandler, IRequestHandler<RegisterCommandRequest, Unit>
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;
		private readonly AuthRules _authRules;

		public RegisterCommandHandler(UserManager<User> userManager, RoleManager<Role> roleManager, IUnitOfWork unitOfWork, ICustomMapper customMapper, IHttpContextAccessor httpContextAccessor, AuthRules authRules) : base(unitOfWork, customMapper, httpContextAccessor)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_authRules = authRules;
		}

		public async Task<Unit> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
		{
			await _authRules.UserShouldNotBeExist(await _userManager.FindByEmailAsync(request.Email));

			User user = customMapper.Map<User, RegisterCommandRequest>(request);
			user.UserName = request.Email;
			user.SecurityStamp = Guid.NewGuid().ToString();

			IdentityResult result = await _userManager.CreateAsync(user, request.Password);

			if (result.Succeeded)
			{
				if (!await _roleManager.RoleExistsAsync("user"))
				{
					await _roleManager.CreateAsync(new Role
					{
						Id = Guid.NewGuid(),
						Name = "user",
						NormalizedName = "USER",
						ConcurrencyStamp = Guid.NewGuid().ToString(),
					});
				}

				await _userManager.AddToRoleAsync(user, "user");
			}

			return Unit.Value;
		}
	}
}
