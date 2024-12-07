using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.DTOs.ModeratorDTO
{
	public class AddModeratorDTO
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int companyId { get; set; }
		public int moderatorId { get; set; }
	}
}
