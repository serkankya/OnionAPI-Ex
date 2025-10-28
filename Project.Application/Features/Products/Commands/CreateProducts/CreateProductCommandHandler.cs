using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Bases;
using Project.Application.Features.Products.Rules;
using Project.Application.Interfaces.CustomAutoMapper;
using Project.Application.Interfaces.UnitOfWorks;
using Project.Domain.Entities;

namespace Project.Application.Features.Products.Commands.CreateProducts
{
	public class CreateProductCommandHandler : BaseHandler, IRequestHandler<CreateProductCommandRequest, Unit>
	{
		private readonly ProductRules _productRules;

		public CreateProductCommandHandler(IUnitOfWork unitOfWork, ProductRules productRules, ICustomMapper customMapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, customMapper, httpContextAccessor)
		{
			_productRules = productRules;
		}

		public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
		{
			IList<Product> products = await unitOfWork.GetReadRepository<Product>().GetAllAsync();

			await _productRules.ProductTitleMustNotBeSame(products, request.Title);

			Product product = new(request.BrandId, request.Title, request.Description, request.Discount, request.Price);

			await unitOfWork.GetWriteRepository<Product>().AddAsync(product);

			if (await unitOfWork.SaveAsync() > 0)
			{
				foreach (var categoryId in request.CategoryIds)
				{
					await unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new()
					{
						ProductId = product.Id,
						CategoryId = categoryId
					});
				}

				await unitOfWork.SaveAsync();
			}

			return Unit.Value;
		}
	}
}
