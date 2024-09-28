using FinalProject.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entities
{
	public class Company : BaseEntity
	{
		public string Name { get; set; }
		public string About { get; set; }
		public string Address { get; set; }
		public string ContactNumber { get; set; }
	


		public ICollection<Vacancy> Vacancies { get; set; }


		public ICollection<Advertising> Advertising { get; set; }

		public ICollection<Subscription> Subscriptions { get; set; }


		public int AppUserId {  get; set; }
		public AppUser AppUser { get; set; }

	}
}
