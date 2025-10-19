using MediatR;

namespace Project.Application.Features.Products.Commands.DeleteProducts
{
	public class DeleteProductCommandRequest : IRequest
	{
		public int Id { get; set; }
	}
}
