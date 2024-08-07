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

		public int IndustryId {  get; set; }
		public Industry Industry { get; set; }
		
	}
}
