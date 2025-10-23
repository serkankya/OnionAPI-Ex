using MediatR;

namespace Project.Application.Features.Products.Commands.DeleteProducts
{
	public class DeleteProductCommandRequest : IRequest<Unit>
	{
		public int Id { get; set; }
	}
}
