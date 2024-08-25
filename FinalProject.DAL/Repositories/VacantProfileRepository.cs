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
	public class VacantProfileRepository : GenericRepositoryApi<VacantProfile>,IVacantProfile
	{
		private readonly AppDBContext context;

		public VacantProfileRepository(AppDBContext context) : base(context)
		{
			this.context = context;
		}
	}
}
