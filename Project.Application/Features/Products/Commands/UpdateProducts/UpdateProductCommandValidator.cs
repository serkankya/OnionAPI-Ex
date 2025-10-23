using FluentValidation;

namespace Project.Application.Features.Products.Commands.UpdateProducts
{
	public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommandRequest>
	{
		public UpdateProductCommandValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty()
				.WithName("Kimlik");

			RuleFor(x => x.BrandId)
				.NotEmpty()
				.WithName("Marka");

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
