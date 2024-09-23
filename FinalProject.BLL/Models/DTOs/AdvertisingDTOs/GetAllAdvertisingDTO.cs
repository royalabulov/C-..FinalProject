using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.DTOs.AdvertisingDTOs
{
	public class GetAllAdvertisingDTO
	{
		public int Id { get; set; }
		public int VacancyId {  get; set; }
		public string? VacancyName { get; set; }
		public decimal Price { get; set; }
        public DateTime StartTime { get; set; }
		public DateTime ExpireTime { get; set; }
		public string TimeLeft {  get; set; }
	}
}
