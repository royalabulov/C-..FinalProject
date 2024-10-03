using AutoMapper;
using FinalProject.BLL.Models.DTOs.CompanyDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Repositories;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;


namespace FinalProject.BLL.Services.Implementation
{
	public class CompanyService : ICompanyService
	{
		private readonly IMapper mapper;
		private readonly IUnitOfWork unitOfWork;
		private readonly UserManager<AppUser> userManager;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly ILogger<CompanyService> logger;

		public CompanyService(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, ILogger<CompanyService> logger)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
			this.userManager = userManager;
			this.httpContextAccessor = httpContextAccessor;
			this.logger = logger;
		}

		public async Task<GenericResponseApi<List<CompanyGetDTO>>> GetAllCompany()
		{
			var response = new GenericResponseApi<List<CompanyGetDTO>>();

			logger.LogInformation("Retrieving all companies...");
			var companyEntity = await unitOfWork.GetRepository<Company>().GetAll();
			if (companyEntity == null)
			{
				response.Failure("Company not found", 404);
				logger.LogWarning("No companies found.");
				return response;
			}
			var mapping = mapper.Map<List<CompanyGetDTO>>(companyEntity);

			response.Success(mapping);

			logger.LogInformation("Successfully retrieved {Count} companies.", mapping.Count);
			return response;
		}

		public async Task<GenericResponseApi<bool>> CreateCompany(CompanyCreateDTO companyCreate)
		{
			var response = new GenericResponseApi<bool>();

			logger.LogInformation("Creating a new company...");

			if (companyCreate == null)
			{
				response.Failure("Company data null", 404);
				logger.LogWarning("Received null company data.");
				return response;
			}
			var mapping = mapper.Map<Company>(companyCreate);
			var currentCompany = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (currentCompany == null)
			{
				response.Failure("currentCompany not found", 404);
				logger.LogWarning("Current company identifier not found.");
				return response;
			}

			mapping.AppUserId = int.Parse(currentCompany);
			await unitOfWork.GetRepository<Company>().AddAsync(mapping);
			await unitOfWork.Commit();
			response.Success(true);
			logger.LogInformation("Company created successfully.");

			return response;
		}

		public async Task<GenericResponseApi<bool>> DeleteCompany(int id)
		{
			var response = new GenericResponseApi<bool>();

			logger.LogInformation("Deleting company with ID {Id}...", id);
			var getById = await unitOfWork.GetRepository<Company>().GetById(id);

			if (getById == null)
			{
				response.Failure("Id not found", 404);
				logger.LogWarning("Company with ID {Id} not found.", id);
				return response;
			}
			unitOfWork.GetRepository<Company>().Remove(getById);
			await unitOfWork.Commit();
			response.Success(true);
			logger.LogInformation("Company with ID {Id} deleted successfully.", id);

			return response;
		}


		public async Task<GenericResponseApi<bool>> UpdateCompany(CompanyUpdateDTO companyUpdate)
		{
			var response = new GenericResponseApi<bool>();

			logger.LogInformation("Updating company with ID {Id}...", companyUpdate.Id);
			var getById = await unitOfWork.GetRepository<Company>().GetById(companyUpdate.Id);

			if (getById == null)
			{
				response.Failure("Id not found", 404);
				logger.LogWarning("Company with ID {Id} not found.", companyUpdate.Id);
				return response;
			}
			var mapping = mapper.Map(companyUpdate, getById);

			unitOfWork.GetRepository<Company>().Update(mapping);
			await unitOfWork.Commit();

			logger.LogInformation("Company with ID {Id} updated successfully.", companyUpdate.Id);
			return response;
		}


	}
}
