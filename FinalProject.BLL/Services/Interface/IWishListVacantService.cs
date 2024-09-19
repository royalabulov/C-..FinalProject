﻿using FinalProject.BLL.Models.DTOs.VacancyDTOs;
using FinalProject.BLL.Models.DTOs.WishListDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;

namespace FinalProject.BLL.Services.Interface
{
	public interface IWishListVacantService
	{
		Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetVacantWishList();
		Task<GenericResponseApi<bool>> AddVacantWishList(AddVacantWishListDTO addVacant);
		Task<GenericResponseApi<bool>> RemoveVacantWishList(int Id);
	}
}
