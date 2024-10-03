using AutoMapper;
using FinalProject.BLL.Models.DTOs.VacancyDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Entities;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.Design;


namespace FinalProject.BLL.Services.Implementation
{
	public class VacancyService : IVacancyService
	{
		private readonly IMapper mapper;
		private readonly IUnitOfWork unitOfWork;
		private readonly ILogger<VacancyService> logger;

		public VacancyService(IMapper mapper, IUnitOfWork unitOfWork,ILogger<VacancyService> logger)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
			this.logger = logger;
		}

		public async Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetAllVacanciesWithPremium()
		{
			var response = new GenericResponseApi<List<GetAllVacancyDTO>>();

			logger.LogInformation("GetAllVacanciesWithPremium method started.");

			var vacancies = await unitOfWork.GetRepository<Vacancy>()
				.GetAsQueryable()
				.Include(x => x.Company)
				.OrderByDescending(v => v.CreateDate)
				.ToListAsync();

			logger.LogInformation($"Retrieved {vacancies.Count} vacancies from the database.");

			var premiumVacancy = new List<Vacancy>();
			var regularVacancy = new List<Vacancy>();

			var currentDate = DateTime.Now;

			var vacancyIds = vacancies.Select(x => x.Id).ToList();

			logger.LogInformation($"Current date: {currentDate}. Checking for active advertisements.");

			var activeAds = await unitOfWork.GetRepository<Advertising>()
				.GetAsQueryable()
				.Where(ad => vacancies.Select(v => v.CompanyId).Contains(ad.CompanyId) && ad.StartTime <= currentDate && ad.ExpireTime >= currentDate)
				.ToListAsync();

			logger.LogInformation($"Retrieved {activeAds.Count} active advertisements.");

			var activeAdsVacancyIds = vacancies
				.Where(v => activeAds.Any(ad => ad.CompanyId == v.CompanyId))
				.Select(v => v.Id)
				.ToHashSet();

			logger.LogInformation($"Found {activeAdsVacancyIds.Count} vacancies with active advertisements.");

			foreach (var vacancy in vacancies)
			{
				if (activeAdsVacancyIds.Contains(vacancy.Id))
				{
					premiumVacancy.Add(vacancy);
				}
				else
				{
					regularVacancy.Add(vacancy);
				}
			}

			logger.LogInformation($"Total premium vacancies: {premiumVacancy.Count}. Total regular vacancies: {regularVacancy.Count}.");

			var result = premiumVacancy.Concat(regularVacancy).ToList();

			var mapping = mapper.Map<List<GetAllVacancyDTO>>(result);

			response.Success(mapping);

			logger.LogInformation("Successfully completed GetAllVacanciesWithPremium method.");
			return response;
		}

	

		public async Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetCompanyVacancy(int companyId)
		{
			var response = new GenericResponseApi<List<GetAllVacancyDTO>>();

			logger.LogInformation($"GetCompanyVacancy method started for companyId: {companyId}.");

			var vacancies = await unitOfWork.GetRepository<Vacancy>().GetAsQueryable()
				.Where(x => x.CompanyId == companyId)
				.OrderByDescending(o => o.CreateDate)
				.ToListAsync();

			logger.LogInformation($"Retrieved {vacancies.Count} vacancies for companyId: {companyId}.");

			if (!vacancies.Any())
			{
				logger.LogWarning($"No vacancies found for companyId: {companyId}.");
				response.Failure("Vacancy not found", 404);
				return response;
			}

			var mapping = mapper.Map<List<GetAllVacancyDTO>>(vacancies);
			response.Success(mapping);

			logger.LogInformation($"Successfully retrieved vacancies for companyId: {companyId}.");
			return response;
		}


		public async Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetCategoryVacancy(int categoryId)
		{
			var response = new GenericResponseApi<List<GetAllVacancyDTO>>();

			logger.LogInformation($"GetCategoryVacancy method started for categoryId: {categoryId}.");

			var categoryVacancy = await unitOfWork.GetRepository<Vacancy>()
				.GetAsQueryable()
				.Where(x => x.CategoryId == categoryId).Include(x => x.Company)
				.ToListAsync();

			logger.LogInformation($"Retrieved {categoryVacancy.Count} vacancies for categoryId: {categoryId}.");

			if (!categoryVacancy.Any())
			{
				logger.LogWarning($"No vacancies found for categoryId: {categoryId}.");
				response.Failure("No vacancies found for this category", 404);
				return response;
			}

			var premiumVacancy = categoryVacancy.Where(x => x.Advertising != null && x.Advertising.ExpireTime >= DateTime.Now).ToList();
			var regularVacancy = categoryVacancy.Where(x => x.Advertising == null || x.Advertising.ExpireTime <= DateTime.Now).ToList();

			var result = premiumVacancy.Concat(regularVacancy).ToList();


			var mapping = mapper.Map<List<GetAllVacancyDTO>>(result);
			response.Success(mapping);

			logger.LogInformation($"Successfully retrieved vacancies for categoryId: {categoryId}. Total vacancies: {result.Count}.");
			return response;
		}



		public async Task<GenericResponseApi<bool>> CreateVacancy(CreateVacancyDTO createVacancy)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				logger.LogInformation("CreateVacancy method started.");

				if (createVacancy == null)
				{
					logger.LogWarning("Vacancy data is null.");
					response.Failure("Vacancy data null", 404);
					return response;
				}

				var category = await unitOfWork.GetRepository<Category>()
					.FirstOrDefaultAsync(c => c.HeaderName == createVacancy.CategoryName);

				if (category == null)
				{
					logger.LogWarning($"Category '{createVacancy.CategoryName}' not found.");
					response.Failure("Category not found.", 404);
					return response;
				}

				var activeAdvertising = await unitOfWork.GetRepository<Advertising>()
					.FirstOrDefaultAsync(x => x.CompanyId == createVacancy.CompanyId && x.ExpireTime > DateTime.Now);



				if (activeAdvertising != null)
				{
					if (createVacancy.AdvertisingId.HasValue)
					{
						var advertisingExists = await unitOfWork.GetRepository<Advertising>()
							.GetAsQueryable()
							.AnyAsync(x => x.Id == createVacancy.AdvertisingId.Value);

						if (!advertisingExists)
						{
							logger.LogWarning($"The specified AdvertisingId {createVacancy.AdvertisingId.Value} does not exist.");
							response.Failure("The specified AdvertisingId does not exist.", 404);
							return response;
						}
					}
				}

				var mapping = mapper.Map<Vacancy>(createVacancy);
				mapping.CategoryId = category.Id;
				mapping.CompanyId = createVacancy.CompanyId;
				mapping.ExpireDate = createVacancy.ExpireDate;

				if (createVacancy.AdvertisingId.HasValue)
				{
					mapping.AdvertisingId = createVacancy.AdvertisingId.Value;
				}
				else
				{
					mapping.AdvertisingId = null;
				}


				if (activeAdvertising != null)
				{
					var premiumExpireDate = DateTime.Now.AddDays((activeAdvertising.ExpireTime - DateTime.Now).TotalDays);

					if (mapping.ExpireDate <= premiumExpireDate)
					{
						logger.LogInformation("Setting vacancy expiration date to premium expiration date.");
					}
					else
					{
						var remainingNormalDuration = (mapping.ExpireDate - premiumExpireDate).TotalDays;
						mapping.ExpireDate = premiumExpireDate.AddDays(remainingNormalDuration);
						logger.LogInformation($"Setting vacancy expiration date to {mapping.ExpireDate} after adjusting for premium duration.");
					}
				}
				else
				{
					mapping.ExpireDate = createVacancy.ExpireDate;
					logger.LogInformation("Setting vacancy expiration date to the provided ExpireDate.");
				}

				await unitOfWork.GetRepository<Vacancy>().AddAsync(mapping);
				await unitOfWork.Commit();
				logger.LogInformation("Vacancy created successfully.");
				response.Success(true);
			}
			catch (Exception ex)
			{
				logger.LogError($"An error occurred while saving the entity changes: {ex.Message}");
				response.Failure($"An error occurred while saving the entity changes: {ex.Message}");
			}

			return response;
		}




		public async Task<GenericResponseApi<bool>> DeleteVacancy(int Id)
		{
			var response = new GenericResponseApi<bool>();

			logger.LogInformation($"DeleteVacancy method started for Id: {Id}");

			var getById = await unitOfWork.GetRepository<Vacancy>().GetById(Id);

			if (getById == null)
			{
				logger.LogInformation($"DeleteVacancy method started for Id: {Id}");
				response.Failure("Id not found", 404);
				return response;
			}
			unitOfWork.GetRepository<Vacancy>().Remove(getById);
			await unitOfWork.Commit();
			logger.LogInformation($"Vacancy with Id {Id} deleted successfully.");

			response.Success(true);
			return response;
		}


		public async Task<GenericResponseApi<bool>> UpdateVacancy(UpdateVacancyDTO updateVacancy)
		{
			var response = new GenericResponseApi<bool>();

			logger.LogInformation($"UpdateVacancy method started for Vacancy Id: {updateVacancy.Id} and Company Id: {updateVacancy.companyId}");

			var updateOwnVacancy = await unitOfWork.GetRepository<Vacancy>()
				.GetAsQueryable()
				.FirstOrDefaultAsync(x => x.CompanyId == updateVacancy.companyId && x.Id == updateVacancy.Id);

			if (updateOwnVacancy == null)
			{
				logger.LogInformation($"UpdateVacancy method started for Vacancy Id: {updateVacancy.Id} and Company Id: {updateVacancy.companyId}");
				response.Failure("Vacancy not found or you do not have permission to update this vacancy", 404);
				return response;
			}

			var mapping = mapper.Map(updateVacancy, updateOwnVacancy);
			mapping.CompanyId = updateVacancy.companyId;

			unitOfWork.GetRepository<Vacancy>().Update(mapping);
			await unitOfWork.Commit();

			logger.LogInformation($"Vacancy with Id {updateVacancy.Id} updated successfully for Company Id: {updateVacancy.companyId}");
			response.Success(true);
			return response;
		}

		public async Task<GenericResponseApi<bool>> DeleteCompanyOwnedVacancy(int Id, int companyId)
		{
			var response = new GenericResponseApi<bool>();

			logger.LogInformation($"DeleteCompanyOwnedVacancy method started for Vacancy Id: {Id} and Company Id: {companyId}");

			var deleteOwnVacancy = await unitOfWork.GetRepository<Vacancy>()
				.FirstOrDefaultAsync(v => v.Id == Id && v.CompanyId == companyId);

			if (deleteOwnVacancy == null)
			{
				logger.LogInformation($"DeleteCompanyOwnedVacancy method started for Vacancy Id: {Id} and Company Id: {companyId}");
				response.Failure("Id not found or you do not have permission to delete this vacancy", 404);
				return response;
			}

			unitOfWork.GetRepository<Vacancy>().Remove(deleteOwnVacancy);
			await unitOfWork.Commit();

			logger.LogInformation($"Vacancy with Id {Id} deleted successfully for Company Id: {companyId}");
			response.Success(true);
			return response;
		}
	}
}
