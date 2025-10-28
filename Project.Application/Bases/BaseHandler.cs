using Microsoft.AspNetCore.Http;
using Project.Application.Interfaces.CustomAutoMapper;
using Project.Application.Interfaces.UnitOfWorks;
using System.Security.Claims;

namespace Project.Application.Bases
{
	public class BaseHandler
	{
		public readonly IUnitOfWork unitOfWork;
		public readonly ICustomMapper customMapper;
		public readonly IHttpContextAccessor httpContextAccessor;
		public readonly string userId;

		public BaseHandler(IUnitOfWork unitOfWork, ICustomMapper customMapper, IHttpContextAccessor httpContextAccessor)
		{
			this.unitOfWork = unitOfWork;
			this.customMapper = customMapper;
			this.httpContextAccessor = httpContextAccessor;
			userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
		}
	}
}
