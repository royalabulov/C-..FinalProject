using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.DTOs.VacancyDTOs
{
	public class GetAllVacancyDTO
	{
		public int Id { get; set; }
		public string HeaderName { get; set; }
		public string Responsibilities {  get; set; }
		public string Requirements {  get; set; }
		public DateTime CreateDate {  get; set; }
		public DateTime ExpireDate { get; set; }
		public string CompanyName { get; set; }
	}
}
