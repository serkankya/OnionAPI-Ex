﻿using MediatR;
using Project.Application.Features.Products.Rules;
using Project.Application.Interfaces.UnitOfWorks;
using Project.Domain.Entities;
using System.Net;

namespace Project.Application.Features.Products.Commands.CreateProducts
{
	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Unit>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ProductRules _productRules;

		public CreateProductCommandHandler(IUnitOfWork unitOfWork, ProductRules productRules)
		{
			_unitOfWork = unitOfWork;
			_productRules = productRules;
		}

		public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
		{
			IList<Product> products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();

			await _productRules.ProductTitleMustNotBeSame(products, request.Title);

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

			return Unit.Value;
		}
	}
}
