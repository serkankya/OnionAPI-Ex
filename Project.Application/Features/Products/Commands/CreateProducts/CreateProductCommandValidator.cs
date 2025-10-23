using FluentValidation;

namespace Project.Application.Features.Products.Commands.CreateProducts
{
	public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
	{
		public CreateProductCommandValidator()
		{
			RuleFor(x => x.BrandId).
				GreaterThan(0).
				WithName("Marka");

			RuleFor(x => x.Title)
				.NotEmpty()
				.WithName("Başlık");

			RuleFor(x => x.Description)
				.NotEmpty()
				.WithName("Açıklama");

			RuleFor(x => x.Discount)
				.GreaterThanOrEqualTo(0)
				.WithName("İndirim");

			RuleFor(x => x.Price)
				.GreaterThan(0)
				.WithName("Fiyat");

			RuleFor(x => x.CategoryIds)
				.NotEmpty()
				.Must(c => c.Any())
				.WithName("Kategoriler");
		}
	}
}
