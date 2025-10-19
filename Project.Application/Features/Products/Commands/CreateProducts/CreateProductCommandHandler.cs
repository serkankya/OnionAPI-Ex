using MediatR;
using Project.Application.Interfaces.UnitOfWorks;
using Project.Domain.Entities;

namespace Project.Application.Features.Products.Commands.CreateProducts
{
	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest>
	{
		private readonly IUnitOfWork _unitOfWork;

		public CreateProductCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
		{
			Product product = new(request.BrandId, request.Title, request.Description, request.Discount, request.Price);

			await _unitOfWork.GetWriteRepository<Product>().AddAsync(product);

			if (await _unitOfWork.SaveAsync() > 0)
			{
				foreach (var categoryId in request.CategoryIds)
				{
					await _unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new()
					{
						ProductId = product.Id,
						CategoryId = categoryId
					});
				}

				await _unitOfWork.SaveAsync();
			}
		}
	}
}
