using MediatR;
using Project.Application.Interfaces.UnitOfWorks;
using Project.Domain.Entities;

namespace Project.Application.Features.Products.Commands.DeleteProducts
{
	public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, Unit>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
		{
			var product = await _unitOfWork.GetReadRepository<Product>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
			product.IsDeleted = true;

			await _unitOfWork.GetWriteRepository<Product>().UpdateAsync(product);
			await _unitOfWork.SaveAsync();

			return Unit.Value;
		}
	}
}
