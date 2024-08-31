using FinalProject.DAL.Context;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL.Repositories.EntityRepository
{
    public class VacanyRepository : GenericRepositoryApi<Vacancy>, IVacanyRepository
    {
        private readonly AppDBContext context;

        public VacanyRepository(AppDBContext context) : base(context)
        {
            this.context = context;
        }

    }
}
