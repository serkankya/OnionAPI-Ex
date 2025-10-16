using Project.Domain.Common;

namespace Project.Domain.Entities
{
	public class Product : EntityBase
	{
		public Product()
		{

		}

		public Product(int brandId, string title, string description, decimal discount, decimal price)
		{
			BrandId = brandId;
			Title = title;
			Description = description;
			Discount = discount;
			Price = price;
		}

		public required int BrandId { get; set; }
		public required string Title { get; set; }
		public required string Description { get; set; }
		public required decimal Discount { get; set; }
		public required decimal Price { get; set; }

		public Brand Brand { get; set; }
		public ICollection<Category> Categories { get; set; }
	}
}
