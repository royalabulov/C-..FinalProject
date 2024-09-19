using AutoMapper;
using FinalProject.BLL.Models.DTOs.WishListDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Repositories;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Implementation
{
	public class WishListVacancyService : IWishListVacancyService
	{
		private readonly IMapper mapper;
		private readonly IUnitOfWork unitOfWork;

		public WishListVacancyService(IMapper mapper, IUnitOfWork unitOfWork)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
		}


		public async Task<GenericResponseApi<List<GetAllVacancyWishListDTO>>> GetVacancyWishList()
		{
			var response = new GenericResponseApi<List<GetAllVacancyWishListDTO>>();

			try
			{
				var wishListEntity = await unitOfWork.GetRepository<WishListVacancy>()
					.GetAsQueryable()
					.Include(x => x.Vacancy)
					.ToListAsync();

				if (wishListEntity == null)
				{
					response.Failure("VacancyWishList data null", 400);
					return response;
				}
				var mapping = mapper.Map<List<GetAllVacancyWishListDTO>>(wishListEntity);
				response.Success(mapping);
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while retrieving wishlist: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> AddVacancyWishList(AddVacancyWishListDTO addVacancyWishListDTO)
		{
			var response = new GenericResponseApi<bool>();

			var vacancyEntity = await unitOfWork.GetRepository<Vacancy>()
				.FirstOrDefaultAsync(x => x.Id == addVacancyWishListDTO.vacancyId);

			var wishListEntity = await unitOfWork.GetRepository<WishListVacancy>()
				.GetAsQueryable()
				.Include(x => x.Vacancy)
				.FirstOrDefaultAsync(y => y.Id == addVacancyWishListDTO.wishListId);


			if (wishListEntity == null)
			{
				wishListEntity = new WishListVacancy
				{
					Id = addVacancyWishListDTO.wishListId,
					Vacancy = new List<Vacancy>()
				};
			}

			await unitOfWork.GetRepository<WishListVacancy>().AddAsync(wishListEntity);


			if (vacancyEntity != null && vacancyEntity != null)
			{
				if (!wishListEntity.Vacancy.Contains(vacancyEntity))
				{
					wishListEntity.Vacancy.Add(vacancyEntity);

					await unitOfWork.Commit();
					response.Success(true);
				}
			}
			else
			{
				response.Success(false);
				response.ErrorMesssage = "Wishlist or Vacancy not found";
			}

			return response;

		}

		public async Task<GenericResponseApi<bool>> RemoveVacancyWishList(int wishListId,int vacancyId)
		{
			var response = new GenericResponseApi<bool>();

			var wishListEntity = await unitOfWork.GetRepository<WishListVacancy>().GetAsQueryable().Include(x=>x.Vacancy).FirstOrDefaultAsync(y=>y.Id == wishListId);
			if (wishListEntity == null)
			{
				response.Failure("wishListId not found", 404);
				return response;
			}

			Vacancy? vacancyRemove = wishListEntity.Vacancy.FirstOrDefault(x => x.Id == vacancyId);
			if (vacancyRemove == null)
			{
				response.Failure("vacancyId not found", 404);
				return response;
			}

			wishListEntity.Vacancy.Remove(vacancyRemove);
			await unitOfWork.Commit();

			response.Success(true);
			return response;
		}

	



		//public async Task<GenericResponseApi<List<GetAllVacantWishListDTO>>> GetVacantWishList()
		//{
		//	var response = new GenericResponseApi<List<GetAllVacantWishListDTO>>();

		//	var wishListEntity = await unitOfWork.GetRepository<WishListVacant>().GetAsQueryable().Include(x => x.VacantProfile).ToListAsync();
		//	if (wishListEntity == null)
		//	{
		//		response.Failure("VacantWishList data null", 400);
		//		return response;
		//	}
		//	var mapping = mapper.Map<List<GetAllVacantWishListDTO>>(wishListEntity);
		//	response.Success(mapping);
		//	return response;
		//}


		//public async Task<GenericResponseApi<bool>> AddVacantWishList(VacantProfile vacantProfile)
		//{
		//	var response = new GenericResponseApi<bool>();
		//	var wishListVacant = new WishListVacant { VacantProfile = vacantProfile };
		//	await unitOfWork.GetRepository<WishListVacant>().AddAsync(wishListVacant);
		//	await unitOfWork.Commit();
		//	return response;
		//}

		//public async Task<GenericResponseApi<bool>> RemoveVacantWishList(int Id)
		//{
		//	var response = new GenericResponseApi<bool>();

		//	var getById = await unitOfWork.GetRepository<WishListVacant>().GetById(Id);
		//	if (getById == null)
		//	{
		//		response.Failure("Id not found", 404);
		//		return response;
		//	}
		//	unitOfWork.GetRepository<WishListVacant>().Remove(getById);
		//	await unitOfWork.Commit();
		//	return response;
		//}

	}
}
