using Project.Application.Bases;
using Project.Application.Features.Products.Exceptions;
using Project.Domain.Entities;

namespace Project.Application.Features.Products.Rules
{
	public class ProductRules : BaseRules
	{
		public Task ProductTitleMustNotBeSame(IList<Product> products, string requestTitle)
		{
			if (products.Any(x => x.Title == requestTitle))
				throw new ProductTitleMustNotBeSameException();

			return Task.CompletedTask;
		}
	}
}
