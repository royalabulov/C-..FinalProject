using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entities
{
	public class Industry : BaseEntity
	{
		public string HeaderName {  get; set; }

		//one to many
		public ICollection<Company> Companies { get; set; }
	}
}
