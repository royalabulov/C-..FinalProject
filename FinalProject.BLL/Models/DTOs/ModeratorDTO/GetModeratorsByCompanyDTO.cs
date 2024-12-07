using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.DTOs.ModeratorDTO
{
	public class GetModeratorsByCompanyDTO
	{
		public int Id {  get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
