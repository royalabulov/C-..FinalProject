using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.DTOs.VacantProfileDTOs
{
	public class CreateVacantProfileDTO
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Skill { get; set; }
		public string About { get; set; }
		public string Language { get; set; }
		public string Number { get; set; }
		public string SocialMedia { get; set; }
		public string Experience { get; set; }
	}
}
