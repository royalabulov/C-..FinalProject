using FinalProject.DAL.Context;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL.Repositories.EntityRepository
{
    public class WishListVacantRepository : GenericRepositoryApi<WishListVacant>, IWishListVacantRepository
    {
		private readonly AppDBContext context;

		public WishListVacantRepository(AppDBContext context) :base(context)
        {
			this.context = context;
		}
    }
}
