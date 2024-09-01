using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.DTOs.AdvertisingDTOs
{
	public class UpdateAdvertisingDTO
	{
		public int Id { get; set; }
		public string? VacancyName { get; set; }
		public int Price { get; set; }
		public DateTime ExpireTime { get; set; }
	}
}
