using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Products.Commands.CreateProducts;
using Project.Application.Features.Products.Commands.DeleteProducts;
using Project.Application.Features.Products.Commands.UpdateProducts;
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
		[Authorize]
		public async Task<IActionResult> GetTest()
		{
			var response = await _mediator.Send(new GetAllProductsQueryRequest());
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
		{
			await _mediator.Send(request);
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest request)
		{
			await _mediator.Send(request);
			return Ok();
		}
		[HttpPost]
		public async Task<IActionResult> DeleteProduct(DeleteProductCommandRequest request)
		{
			await _mediator.Send(request);
			return Ok();
		}
	}
}
