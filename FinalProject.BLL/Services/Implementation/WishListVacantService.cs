using AutoMapper;
using FinalProject.BLL.Models.DTOs.VacancyDTOs;
using FinalProject.BLL.Models.DTOs.WishListDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Entities;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Implementation
{
	public class WishListVacantService : IWishListVacantService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public WishListVacantService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}



		public async Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetVacantWishList()
		{
			var response = new GenericResponseApi<List<GetAllVacancyDTO>>();

			var vacant = await unitOfWork.GetRepository<VacantProfile>()
				.GetAsQueryable()
				.Include(wl => wl.WishListVacants)
				.ThenInclude(v=>v.Vacancy).ToListAsync();

			if(vacant == null)
			{
				response.Failure("No vacant profiles found", 404);
				return response;
			}

			var mapping = mapper.Map<List<GetAllVacancyDTO>>(vacant.Select(x=>x.WishListVacants).ToList().Distinct().ToList());

			response.Success(mapping);
			return response;
		}


		public async Task<GenericResponseApi<bool>> AddVacantWishList(AddVacantWishListDTO addVacant)
		{
			var response = new GenericResponseApi<bool>();

			var vacant = await unitOfWork.GetRepository<VacantProfile>()
				.FirstOrDefaultAsync(x => x.Id == addVacant.VacantProfileId);

			var vacancy = await unitOfWork.GetRepository<Vacancy>()
				.FirstOrDefaultAsync(x => x.Id == addVacant.VacancyId);

			if (vacancy == null || vacant == null)
			{
				response.Failure("Vacant or Vacancy not found", 404);
				return response;
			}

			 var wishListProfile = mapper.Map<WishListVacant>(addVacant);

			await unitOfWork.GetRepository<WishListVacant>().AddAsync(wishListProfile);
			await unitOfWork.Commit();
			response.Success(true);

			return response;


		}

		public Task<GenericResponseApi<bool>> RemoveVacantWishList(int Id)
		{
			throw new NotImplementedException();
		}
	}	
}
