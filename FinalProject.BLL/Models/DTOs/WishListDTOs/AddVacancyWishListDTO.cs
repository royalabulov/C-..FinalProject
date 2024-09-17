using FinalProject.Domain.Entites;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.DTOs.WishListDTOs
{
	public class AddVacancyWishListDTO
	{
		public int wishListId { get; set; }
		public int vacancyId { get; set; }
	}
}
