using FluentValidation;

namespace Project.Application.Features.Products.Commands.DeleteProducts
{
	public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommandRequest>
	{
		public DeleteProductCommandValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty()
				.WithName("Kimlik değeri");
		}
	}
}
