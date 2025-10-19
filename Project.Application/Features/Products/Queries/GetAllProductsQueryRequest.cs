using MediatR;

namespace Project.Application.Features.Products.Queries
{
	public class GetAllProductsQueryRequest : IRequest<IList<GetAllProductsQueryResponse>>
	{
	}
}
