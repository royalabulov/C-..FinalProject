using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.DTOs.SubscriptionDTOs
{
	public class CreateSubscriptionDTO
	{
		public string? HeaderName { get; set; }
		public int Price { get; set; }
		public string? SubscriptionLevel { get; set; }
	}
}
