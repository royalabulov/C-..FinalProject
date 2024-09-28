using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entites
{
	public class Advertising : BaseEntity
	{
		public decimal Price {  get; set; }
		public DateTime StartTime { get; set; }
		public DateTime ExpireTime { get; set; }

		public Company Company { get; set; }
		public int CompanyId { get; set; }


		public ICollection<Vacancy> Vacancy { get; set; }
	}
}
