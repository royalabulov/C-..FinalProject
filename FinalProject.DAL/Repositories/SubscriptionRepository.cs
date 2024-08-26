using FinalProject.DAL.Context;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL.Repositories
{
	public class SubscriptionRepository : GenericRepositoryApi<Subscription>, ISubscriptionRepository
	{
		private readonly AppDBContext context;

		public SubscriptionRepository(AppDBContext context) : base(context)
		{
			this.context = context;
		}
	}
}
