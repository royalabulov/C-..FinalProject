using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entities
{
	public class Category : BaseEntity
	{
		public string? HeaderName { get; set; }

		//one to many
        public ICollection<Vacancy>? Vacancy { get; set; }



	}
}
