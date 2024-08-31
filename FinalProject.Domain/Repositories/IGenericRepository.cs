using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Repositories
{
	public interface IGenericRepository<T> where T : class 
	{
		Task<T> GetById(int id);
		Task<IEnumerable<T>> GetAll();
		IQueryable<T> GetAsQueryable();
		Task AddAsync(T entity);
		void Remove(T entity);
		void Update(T entity);


	}
}
