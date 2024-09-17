using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entites
{
	public class Subscription : BaseEntity
	{
		public string? HeaderName {  get; set; }
		public int Price {  get; set; }
		public string? SubscriptionLevel { get; set; }
		public DateTime SubscriptionExpireTime { get; set; }



		public int CompanyId { get; set; }
		public Company? Company { get; set; }
	
	}
}
