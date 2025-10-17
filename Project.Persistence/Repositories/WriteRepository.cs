using Microsoft.EntityFrameworkCore;
using Project.Application.Interfaces.Repositories;
using Project.Domain.Common;

namespace Project.Persistence.Repositories
{
	public class WriteRepository<T> : IWriteRepository<T> where T : class, IEntityBase, new()
	{
		private readonly DbContext _context;

		public WriteRepository(DbContext context)
		{
			_context = context;
		}

		private DbSet<T> Table => _context.Set<T>();

		public async Task AddAsync(T entity)
		{
			await Table.AddAsync(entity);
		}

		public async Task AddRangeAsync(IList<T> entities)
		{
			await Table.AddRangeAsync(entities);
		}

		public Task HardDeleteAsync(T entity)
		{
			Table.Remove(entity);
			return Task.CompletedTask;
		}

		public Task<T> UpdateAsync(T entity)
		{
			Table.Update(entity);
			return Task.FromResult(entity);
		}
	}
}
