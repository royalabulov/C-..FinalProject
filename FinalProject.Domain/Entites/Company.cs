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
		public DateTime SubscriptionExpireTime { get; set; }


		public ICollection<Vacancy> Vacancies { get; set; }

		public int IndustryId { get; set; }
		public Industry Industry { get; set; }


		//one to one
	
		public Subscription Subscription { get; set; }

	    public int AppUserId {  get; set; }
		public AppUser AppUser { get; set; }

	}
}
