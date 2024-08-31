using FinalProject.DAL.Context;
using FinalProject.DAL.Repositories;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Repositories;
using FinalProject.Domain.UnitOfWorkInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL.UnitOfWorkImplementation
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDBContext context;
		private Dictionary<Type, object> repositories;

		public UnitOfWork(AppDBContext context)
        {
			this.context = context;
			repositories = new Dictionary<Type, object>();
		}
        public async Task<int> Commit()
		{
			return await context.SaveChangesAsync();
		}

		public void Dispose()
		{
			context.Dispose();
		}

		public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
		{
			if (repositories.ContainsKey(typeof(TEntity)))
			{
				return (IGenericRepository<TEntity>)repositories[typeof(TEntity)];
			}

			IGenericRepository<TEntity> repository = new GenericRepositoryApi<TEntity>(context);
			repositories.Add(typeof(TEntity), repository);
			return repository;
		}
	}
}
