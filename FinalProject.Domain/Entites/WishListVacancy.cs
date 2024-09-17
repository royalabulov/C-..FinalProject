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
		
		public ICollection<Vacancy> Vacancy { get; set; }

	}
}
