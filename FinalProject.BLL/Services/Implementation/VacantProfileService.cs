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
using Microsoft.Extensions.Logging;
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
		private readonly ILogger<VacantProfileService> logger;

		public VacantProfileService(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor,ILogger<VacantProfileService> logger)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
			this.userManager = userManager;
			this.httpContextAccessor = httpContextAccessor;
			this.logger = logger;
		}

		public async Task<GenericResponseApi<List<GetAllVacantDTO>>> AllVacant()
		{
			var response = new GenericResponseApi<List<GetAllVacantDTO>>();

			logger.LogInformation("AllVacant method started.");

			var vacantEntity = await unitOfWork.GetRepository<VacantProfile>().GetAll();
			if (vacantEntity == null)
			{
				logger.LogWarning("VacantProfile not found.");
				response.Failure("VacantProfile not found", 404);
				return response;
			}
			var mapping = mapper.Map<List<GetAllVacantDTO>>(vacantEntity);
			response.Success(mapping);

			logger.LogInformation("AllVacant method completed successfully.");
			return response;
		}

		public async Task<GenericResponseApi<bool>> CreateVacantProfile(CreateVacantProfileDTO createVacantProfile)
		{
			var response = new GenericResponseApi<bool>();

			logger.LogInformation("CreateVacantProfile method started.");

			if (createVacantProfile == null)
			{
				logger.LogWarning("VacantProfile data is null.");
				response.Failure("VacantProfile data null", 404);
				return response;
			}
			var mapping = mapper.Map<VacantProfile>(createVacantProfile);

			var currentUser = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (currentUser == null)
			{
				logger.LogWarning("Current user not found.");
				response.Failure("currentCompany not found", 404);
				return response;
			}

			mapping.AppUserId = int.Parse(currentUser);

			await unitOfWork.GetRepository<VacantProfile>().AddAsync(mapping);
			await unitOfWork.Commit();
			response.Success(true);

			logger.LogInformation("CreateVacantProfile method completed successfully.");
			return response;
		}



		public async Task<GenericResponseApi<bool>> DeleteVacant(int id)
		{
			var response = new GenericResponseApi<bool>();

			logger.LogInformation($"DeleteVacant method started for VacantProfile Id: {id}");

			var getById = await unitOfWork.GetRepository<VacantProfile>().GetById(id);
			if (getById == null)
			{
				logger.LogWarning($"VacantProfile with Id {id} not found.");
				response.Failure("Id not found", 404);
				return response;
			}
			unitOfWork.GetRepository<VacantProfile>().Remove(getById);
			await unitOfWork.Commit();
			response.Success(true);

			logger.LogInformation($"VacantProfile with Id {id} deleted successfully.");
			return response;
		}

		public async Task<GenericResponseApi<bool>> UpdateVacantProfile(UpdateVacantProfileDTO updateVacantProfile)
		{
			var response = new GenericResponseApi<bool>();

			logger.LogInformation($"UpdateVacantProfile method started for VacantProfile Id: {updateVacantProfile.Id}");

			var getById = await unitOfWork.GetRepository<VacantProfile>().GetById(updateVacantProfile.Id);
			if (getById == null)
			{
				logger.LogWarning($"VacantProfile with Id {updateVacantProfile.Id} not found.");
				response.Failure("Id not found", 404);
				return response;
			}

			var mapping = mapper.Map(updateVacantProfile, getById);
			unitOfWork.GetRepository<VacantProfile>().Update(mapping);
			await unitOfWork.Commit();
			response.Success(true);

			logger.LogInformation($"VacantProfile with Id {updateVacantProfile.Id} updated successfully.");
			return response;
		}
	}
}
