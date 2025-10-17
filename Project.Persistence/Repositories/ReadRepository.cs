﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Project.Application.Interfaces.Repositories;
using Project.Domain.Common;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories
{
	public class ReadRepository<T> : IReadRepository<T> where T : class, IEntityBase, new()
	{
		private readonly DbContext _context;

		public ReadRepository(DbContext context)
		{
			_context = context;
		}

		private DbSet<T> Table { get => _context.Set<T>(); }

		public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
		{
			return await (predicate is null
				? Table.AsNoTracking().CountAsync()
				: Table.AsNoTracking().Where(predicate).CountAsync());
		}

		public  IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false)
		{
			return enableTracking
				? Table.Where(predicate)
				: Table.AsNoTracking().Where(predicate);
		}

		public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
		{
			IQueryable<T> queryable = Table;
			if (!enableTracking) queryable = queryable.AsNoTracking();
			if (include is not null) queryable = include(queryable);
			if (predicate is not null) queryable = queryable.Where(predicate);
			if (orderBy is not null) return await orderBy(queryable).ToListAsync();

			return await queryable.ToListAsync();
		}

		public async Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false, int currentPage = 1, int pageSize = 3)
		{
			IQueryable<T> queryable = Table;
			if (!enableTracking) queryable = queryable.AsNoTracking();
			if (include is not null) queryable = include(queryable);
			if (predicate is not null) queryable = queryable.Where(predicate);
			if (orderBy is not null)
				return await orderBy(queryable).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

			return await queryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
		}

		public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
		{
			IQueryable<T> queryable = Table;
			if (!enableTracking) queryable = queryable.AsNoTracking();
			if (include is not null) queryable = include(queryable);

			return await queryable.FirstOrDefaultAsync(predicate);
		}
	}
}
