using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.DTOs.WishListDTOs
{
	public class GetAllWishListDTO
	{
		public int Id { get; set; }
		public string VacancyName {  get; set; }
		public string VacantProfile {  get; set; }
	}
}
