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

		public int BrandId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Discount { get; set; }
		public decimal Price { get; set; }

		public Brand Brand { get; set; }
		public ICollection<Category> Categories { get; set; }
	}
}
