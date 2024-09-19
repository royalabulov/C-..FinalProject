using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entites
{
    public class WishListVacant : BaseEntity
    {
		public int VacantProfileId { get; set; }
	    public VacantProfile? VacantProfile { get; set; }

		public int VacancyId { get; set; }
		public Vacancy? Vacancy { get; set; }
	}
}
