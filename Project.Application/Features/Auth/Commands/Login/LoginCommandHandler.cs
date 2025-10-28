using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Project.Application.Bases;
using Project.Application.Features.Auth.Rules;
using Project.Application.Interfaces.CustomAutoMapper;
using Project.Application.Interfaces.Tokens;
using Project.Application.Interfaces.UnitOfWorks;
using Project.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Project.Application.Features.Auth.Commands.Login
{
	public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommandRequest, LoginCommandResponse>
	{
		readonly UserManager<User> _userManager;
		readonly AuthRules _authRules;
		readonly IConfiguration _configuration;
		readonly ITokenService _tokenService;

		public LoginCommandHandler(IUnitOfWork unitOfWork, ICustomMapper customMapper, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, AuthRules authRules, IConfiguration configuration, ITokenService tokenService) : base(unitOfWork, customMapper, httpContextAccessor)
		{
			_userManager = userManager;
			_authRules = authRules;
			_configuration = configuration;
			_tokenService = tokenService;
		}
		public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
		{
			User user = await _userManager.FindByEmailAsync(request.Email);
			bool checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);

			await _authRules.EmailOrPasswordShouldNotBeInvalid(user, checkPassword);

			IList<string> roles = await _userManager.GetRolesAsync(user);

			JwtSecurityToken token = await _tokenService.CreateToken(user, roles);
			string refreshToken = _tokenService.GenerateRefreshToken();

			_ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

			user.RefreshToken = refreshToken;
			user.RefreshTokenExpiryDate = DateTime.Now.AddDays(refreshTokenValidityInDays); 

			await _userManager.UpdateAsync(user);
			await _userManager.UpdateSecurityStampAsync(user);

			string _token = new JwtSecurityTokenHandler().WriteToken(token);

			await _userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", _token);

			return new()
			{
				Token = _token,
				RefreshToken = refreshToken,
				Expiration = token.ValidTo
			};
		}
	}
}
