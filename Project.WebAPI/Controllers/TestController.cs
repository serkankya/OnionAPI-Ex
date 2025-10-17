using Microsoft.AspNetCore.Mvc;
using Project.Application.UnitOfWorks;
using Project.Domain.Entities;

namespace Project.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;

		public TestController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public async Task<IActionResult> GetTest()
		{
			return Ok(await _unitOfWork.GetReadRepository<Product>().GetAllAsync());
		}
	}
}
