using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project.Application.Bases;
using Project.Application.Features.Auth.Rules;
using Project.Application.Interfaces.CustomAutoMapper;
using Project.Application.Interfaces.Tokens;
using Project.Application.Interfaces.UnitOfWorks;
using Project.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Project.Application.Features.Auth.Commands.RefreshToken
{
	public class RefreshTokenCommandHandler : BaseHandler, IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
	{
		readonly UserManager<User> _userManager;
		readonly AuthRules _authRules;
		readonly ITokenService _tokenService;

		public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, ICustomMapper customMapper, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, AuthRules authRules, ITokenService tokenService) : base(unitOfWork, customMapper, httpContextAccessor)
		{
			_userManager = userManager;
			_authRules = authRules;
			_tokenService = tokenService;
		}

		public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
		{
			ClaimsPrincipal? principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);

			string email = principal.FindFirstValue(ClaimTypes.Email);

			User? user = await _userManager.FindByEmailAsync(email);
			IList<string> roles = await _userManager.GetRolesAsync(user);

			await _authRules.RefreshTokenShouldNotBeExpired(user.RefreshTokenExpiryDate);

			JwtSecurityToken newAccessToken = await _tokenService.CreateToken(user, roles);
			string newRefreshToken = _tokenService.GenerateRefreshToken();

			user.RefreshToken = newRefreshToken;
			await _userManager.UpdateAsync(user);

			return new()
			{
				AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
				RefreshToken = newRefreshToken
			};
		}
	}
}
