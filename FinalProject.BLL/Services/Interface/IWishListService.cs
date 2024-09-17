using FinalProject.BLL.Models.DTOs.WishListDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Interface
{
	public interface IWishListService
	{
		Task<GenericResponseApi<List<GetAllVacantWishListDTO>>> GetVacantWishList();
		Task<GenericResponseApi<bool>> AddVacantWishList(VacantProfile vacantProfile);
		Task<GenericResponseApi<bool>> RemoveVacantWishList(int Id);


		Task<GenericResponseApi<List<GetAllVacancyWishListDTO>>> GetVacancyWishList();
		Task<GenericResponseApi<bool>> AddVacancyWishList(AddVacancyWishListDTO addVacancyWishListDTO);
		Task<GenericResponseApi<bool>> RemoveVacancyWishList(int id);


	}
}
