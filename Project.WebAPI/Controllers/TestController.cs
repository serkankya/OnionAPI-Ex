using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Products.Queries;

namespace Project.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly IMediator _mediator;

		public TestController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetTest()
		{
			var response = await _mediator.Send(new GetAllProductsQueryRequest());
			return Ok(response);
		}


	}
}
