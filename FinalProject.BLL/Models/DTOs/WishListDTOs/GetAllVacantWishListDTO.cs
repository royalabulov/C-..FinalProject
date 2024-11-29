using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.DTOs.WishListDTOs
{
	public class GetAllVacantWishListDTO
	{
		public int VacantProfileId { get; set; }
		public string VacantProfileName { get; set; }
		public List<VacancyDetailsDTO> VacancyDetails { get; set; }
	}
}
