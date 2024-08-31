using FinalProject.Domain.Entities;
using FinalProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.UnitOfWorkInterface
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;

		Task<int> Commit();
	}
}
