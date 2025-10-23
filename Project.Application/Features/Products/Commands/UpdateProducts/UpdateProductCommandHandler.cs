using MediatR;
using Project.Application.Interfaces.CustomAutoMapper;
using Project.Application.Interfaces.UnitOfWorks;
using Project.Domain.Entities;

namespace Project.Application.Features.Products.Commands.UpdateProducts
{
	public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, Unit>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICustomMapper _mapper;

		public UpdateProductCommandHandler(IUnitOfWork unitOfWork, ICustomMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
		{
			var product = await _unitOfWork.GetReadRepository<Product>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);

			var map = _mapper.Map<Product, UpdateProductCommandRequest>(request);

			var productCategories = await _unitOfWork.GetReadRepository<ProductCategory>()
				.GetAllAsync(x => x.ProductId == product.Id);

			await _unitOfWork.GetWriteRepository<ProductCategory>()
				.HardDeleteRangeAsync(productCategories);

			foreach (var categoryId in request.CategoryIds)
				await _unitOfWork.GetWriteRepository<ProductCategory>()
					.AddAsync(new() { CategoryId = categoryId, ProductId = product.Id });

			await _unitOfWork.GetWriteRepository<Product>().UpdateAsync(map);
			await _unitOfWork.SaveAsync();

			return Unit.Value;
		}
	}
}
