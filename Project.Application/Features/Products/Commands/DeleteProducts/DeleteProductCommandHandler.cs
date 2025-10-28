using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Bases;
using Project.Application.Interfaces.CustomAutoMapper;
using Project.Application.Interfaces.UnitOfWorks;
using Project.Domain.Entities;

namespace Project.Application.Features.Products.Commands.DeleteProducts
{
	public class DeleteProductCommandHandler : BaseHandler, IRequestHandler<DeleteProductCommandRequest, Unit>
	{
		public DeleteProductCommandHandler(IUnitOfWork unitOfWork, ICustomMapper customMapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, customMapper, httpContextAccessor)
		{
		}

		public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
		{
			var product = await unitOfWork.GetReadRepository<Product>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
			product.IsDeleted = true;

			await unitOfWork.GetWriteRepository<Product>().UpdateAsync(product);
			await unitOfWork.SaveAsync();

			return Unit.Value;
		}
	}
}
