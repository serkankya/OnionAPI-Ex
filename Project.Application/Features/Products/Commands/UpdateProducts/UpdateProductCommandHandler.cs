using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Bases;
using Project.Application.Interfaces.CustomAutoMapper;
using Project.Application.Interfaces.UnitOfWorks;
using Project.Domain.Entities;

namespace Project.Application.Features.Products.Commands.UpdateProducts
{
	public class UpdateProductCommandHandler : BaseHandler, IRequestHandler<UpdateProductCommandRequest, Unit>
	{
		public UpdateProductCommandHandler(IUnitOfWork unitOfWork, ICustomMapper customMapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, customMapper, httpContextAccessor)
		{
		}

		public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
		{
			var product = await unitOfWork.GetReadRepository<Product>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);

			var map = customMapper.Map<Product, UpdateProductCommandRequest>(request);

			var productCategories = await unitOfWork.GetReadRepository<ProductCategory>()
				.GetAllAsync(x => x.ProductId == product.Id);

			await unitOfWork.GetWriteRepository<ProductCategory>()
				.HardDeleteRangeAsync(productCategories);

			foreach (var categoryId in request.CategoryIds)
				await unitOfWork.GetWriteRepository<ProductCategory>()
					.AddAsync(new() { CategoryId = categoryId, ProductId = product.Id });

			await unitOfWork.GetWriteRepository<Product>().UpdateAsync(map);
			await unitOfWork.SaveAsync();

			return Unit.Value;
		}
	}
}
