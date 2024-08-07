using FinalProject.DAL.Context;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL.Repositories
{
	public class IndustryRepository :GenericRepository<Industry>,IIndustryRepository 
	{
		private readonly AppDBContext context;

		public IndustryRepository(AppDBContext context) : base(context)
        {
			this.context = context;
		}
    }
}
