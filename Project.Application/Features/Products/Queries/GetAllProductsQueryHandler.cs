using MediatR;
using Project.Application.Interfaces.UnitOfWorks;
using Project.Domain.Entities;

namespace Project.Application.Features.Products.Queries
{
	public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
		{
			var products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();

			List<GetAllProductsQueryResponse> response = new();

			foreach (var product in products)
			{
				response.Add(new GetAllProductsQueryResponse
				{
					Title = product.Title,
					Description = product.Description,
					Discount = product.Discount,
					Price = product.Price - (product.Price * product.Discount / 100)
				});
			}

			return response;
		}
	}
}
