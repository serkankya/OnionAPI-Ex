using Project.Application.Bases;

namespace Project.Application.Features.Products.Exceptions
{
	public class ProductTitleMustNotBeSameException : BaseExceptions
	{
		public ProductTitleMustNotBeSameException() : base("Bu ürün adı zaten kullanılmış!")
		{
		}
	}
}
