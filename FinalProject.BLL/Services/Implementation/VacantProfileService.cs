using AutoMapper;
using FinalProject.BLL.Models.DTOs.VacantProfileDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Repositories;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Implementation
{
	public class VacantProfileService : IVacantProfileService
	{	
		private readonly IMapper mapper;
		private readonly IUnitOfWork unitOfWork;
		private readonly UserManager<AppUser> userManager;
		private readonly IHttpContextAccessor httpContextAccessor;

		public VacantProfileService(IMapper mapper,IUnitOfWork unitOfWork,UserManager<AppUser> userManager,IHttpContextAccessor httpContextAccessor)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
			this.userManager = userManager;
			this.httpContextAccessor = httpContextAccessor;
		}

		public async Task<GenericResponseApi<List<GetAllVacantDTO>>> AllVacant()
		{
			var response = new GenericResponseApi<List<GetAllVacantDTO>>();

			try
			{
				var vacantEntity = await unitOfWork.GetRepository<VacantProfile>().GetAll();
				if (vacantEntity == null)
				{
					response.Failure("VacantProfile not found", 404);
					return response;
				}
				var mapping = mapper.Map<List<GetAllVacantDTO>>(vacantEntity);
				response.Success(mapping);
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while retrieving VacantProfile: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> CreateVacantProfile(CreateVacantProfileDTO createVacantProfile)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
                 
				if (createVacantProfile == null)
				{
					response.Failure("VacantProfile data null", 404);
					return response;
				}
				var mapping = mapper.Map<VacantProfile>(createVacantProfile);

				var currentUser = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

				if (currentUser == null)
				{
					response.Failure("currentCompany not found", 404);
					return response;
				}

				mapping.AppUserId = int.Parse(currentUser);

				await unitOfWork.GetRepository<VacantProfile>().AddAsync(mapping);
				await unitOfWork.Commit();
				response.Success(true);
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while create VacantProfile: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}



		public async Task<GenericResponseApi<bool>> DeleteVacant(int id)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var getById = await unitOfWork.GetRepository<VacantProfile>().GetById(id);
				if (getById == null)
				{
					response.Failure("Id not found", 404);
					return response;
				}
				unitOfWork.GetRepository<VacantProfile>().Remove(getById);
				await unitOfWork.Commit();
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while deleting the VacantProfile: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> UpdateVacantProfile(UpdateVacantProfileDTO updateVacantProfile)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var getById = await unitOfWork.GetRepository<VacantProfile>().GetById(updateVacantProfile.Id);
				if(getById == null)
				{
					response.Failure("Id not found", 404);
					return response;
				}
				unitOfWork.GetRepository<VacantProfile>().Update(getById);
				await unitOfWork.Commit();
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while updating the Vacancy: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}
	}
}
