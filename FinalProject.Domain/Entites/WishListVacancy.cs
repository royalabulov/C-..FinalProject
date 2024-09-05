using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entites
{
	public class WishListVacancy : BaseEntity
	{
		//one to one
		public int VacancyId {  get; set; }
		public Vacancy Vacancy { get; set;}

	}
}
