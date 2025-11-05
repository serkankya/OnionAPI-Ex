using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Auth.Commands.Login;
using Project.Application.Features.Auth.Commands.RefreshToken;
using Project.Application.Features.Auth.Commands.Register;

namespace Project.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AuthController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterCommandRequest request)
		{
			await _mediator.Send(request);
			return StatusCode(StatusCodes.Status201Created);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginCommandRequest request)
		{
			var res = await _mediator.Send(request);
			return StatusCode(StatusCodes.Status200OK, res);
		}

		[HttpPost]
		public async Task<IActionResult> RefreshToken(RefreshTokenCommandRequest request)
		{
			var res = await _mediator.Send(request);
			return StatusCode(StatusCodes.Status200OK, res);
		}
	}
}
