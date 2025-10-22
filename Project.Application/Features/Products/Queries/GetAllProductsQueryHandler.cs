using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs.BrandDTOs;
using Project.Application.Interfaces.CustomAutoMapper;
using Project.Application.Interfaces.UnitOfWorks;
using Project.Domain.Entities;

namespace Project.Application.Features.Products.Queries
{
	public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICustomMapper _mapper;

		public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, ICustomMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
		{
			var products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync(include: x => x.Include(b => b.Brand));

			var brand = _mapper.Map<BrandDto, Brand>(new Brand());

			var map = _mapper.Map<GetAllProductsQueryResponse, Product>(products);

			foreach (var item in map)
				item.Price -= (item.Price * item.Discount / 100);

			throw new Exception("Ürünler listelenirken bir hata oluştu.");
		}
	}
}
