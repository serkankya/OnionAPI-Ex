using MediatR;

namespace Project.Application.Features.Products.Commands.UpdateProducts
{
	public class UpdateProductCommandRequest : IRequest<Unit>
	{
		public int Id { get; set; }
		public int BrandId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Discount { get; set; }
		public decimal Price { get; set; }
		public IList<int> CategoryIds { get; set; }
	}
}
