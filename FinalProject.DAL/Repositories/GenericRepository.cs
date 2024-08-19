using FinalProject.DAL.Context;
using FinalProject.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL.Repositories
{
	public class GenericRepositoryApi<T> : IGenericRepository<T> where T : class
	{
		private readonly AppDBContext context;
		private readonly DbSet<T> table;
		public GenericRepositoryApi(AppDBContext context)
        {
			this.context = context;
			table = context.Set<T>();
		}

        public async Task AddAsync(T entity)
		{
			await table.AddAsync(entity);
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			return await table.ToListAsync();
		}

		public async Task<T> GetById(int id)
		{
			return await table.FindAsync(id);
		}

		public void Remove(T entity)
		{
			table.Remove(entity);
		}

		public void Update(T entity)
		{
			table.Update(entity);
		}
	}
}
