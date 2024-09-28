using AutoMapper;
using FinalProject.BLL.Models.DTOs.VacancyDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.DAL.Context;
using FinalProject.DAL.Repositories;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Entities;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.BLL.Services.Implementation
{
	public class VacancyService : IVacancyService
	{
		private readonly IMapper mapper;
		private readonly IUnitOfWork unitOfWork;


		public VacancyService(IMapper mapper, IUnitOfWork unitOfWork)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;

		}
		#region butun vacancylari getirir

		//public async Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetAllVacancy()
		//{
		//	var response = new GenericResponseApi<List<GetAllVacancyDTO>>();

		//	try
		//	{

		//		var vacancyEntity = await unitOfWork.GetRepository<Vacancy>().GetAll();



		//		if (vacancyEntity == null)
		//		{
		//			response.Failure("Vacancy not found", 404);
		//			return response;
		//		}


		//		var mapping = mapper.Map<List<GetAllVacancyDTO>>(vacancyEntity);

		//		response.Success(mapping);


		//	}
		//	catch (Exception ex)
		//	{
		//		response.Failure($"An error occurred while retrieving Vacancy: {ex.Message}");
		//		Console.WriteLine(ex.Message);
		//	}
		//	return response;
		//}

		#endregion

		public async Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetAllVacanciesWithPremium()
		{
			var response = new GenericResponseApi<List<GetAllVacancyDTO>>();


			var vacancies = await unitOfWork.GetRepository<Vacancy>()
				.GetAsQueryable()
				.Include(x => x.Company)
				.OrderByDescending(v => v.CreateDate)
				.ToListAsync();

			var premiumVacancy = new List<Vacancy>();
			var regularVacancy = new List<Vacancy>();

			var currentDate = DateTime.Now;

			var vacancyIds = vacancies.Select(x => x.Id).ToList();

			var activeAds = await unitOfWork.GetRepository<Advertising>()
				.GetAsQueryable()
				.Where(ad => vacancies.Select(v => v.CompanyId).Contains(ad.CompanyId) && ad.StartTime <= currentDate && ad.ExpireTime >= currentDate)
				.ToListAsync();

			var activeAdsVacancyIds = vacancies
				.Where(v => activeAds.Any(ad => ad.CompanyId == v.CompanyId))
				.Select(v => v.Id)
				.ToHashSet();


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


			var result = premiumVacancy.Concat(regularVacancy).ToList();

			var mapping = mapper.Map<List<GetAllVacancyDTO>>(result);

			response.Success(mapping);

			return response;
		}

	

		public async Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetCompanyVacancy(int compnayId)
		{
			var response = new GenericResponseApi<List<GetAllVacancyDTO>>();

			var vacancies = await unitOfWork.GetRepository<Vacancy>().GetAsQueryable()
				.Where(x => x.CompanyId == compnayId)
				.OrderByDescending(o => o.CreateDate)
				.ToListAsync();

			if (!vacancies.Any())
			{
				response.Failure("Vacancy not found", 404);
				return response;
			}

			var mapping = mapper.Map<List<GetAllVacancyDTO>>(vacancies);
			response.Success(mapping);

			return response;
		}


		public async Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetCategoryVacancy(int categoryId)
		{
			var response = new GenericResponseApi<List<GetAllVacancyDTO>>();

			var categoryVacancy = await unitOfWork.GetRepository<Vacancy>()
				.GetAsQueryable()
				.Where(x => x.CategoryId == categoryId).Include(x => x.Company)
				.ToListAsync();

			if (!categoryVacancy.Any())
			{
				response.Failure("No vacancies found for this category", 404);
				return response;
			}

			var premiumVacancy = categoryVacancy.Where(x => x.Advertising != null && x.Advertising.ExpireTime >= DateTime.Now).ToList();
			var regularVacancy = categoryVacancy.Where(x => x.Advertising == null || x.Advertising.ExpireTime <= DateTime.Now).ToList();

			var result = premiumVacancy.Concat(regularVacancy).ToList();


			var mapping = mapper.Map<List<GetAllVacancyDTO>>(result);
			response.Success(mapping);

			return response;
		}

		public async Task<GenericResponseApi<bool>> CreateVacancy(CreateVacancyDTO createVacancy)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				if (createVacancy == null)
				{
					response.Failure("Vacancy data null", 404);
					return response;
				}

				var category = await unitOfWork.GetRepository<Category>()
					.FirstOrDefaultAsync(c => c.HeaderName == createVacancy.CategoryName);

				if (category == null)
				{
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

					}
					else
					{
						var remainingNormalDuration = (mapping.ExpireDate - premiumExpireDate).TotalDays;
						mapping.ExpireDate = premiumExpireDate.AddDays(remainingNormalDuration);
					}
				}
				else
				{
					mapping.ExpireDate = createVacancy.ExpireDate;
				}




				await unitOfWork.GetRepository<Vacancy>().AddAsync(mapping);
				await unitOfWork.Commit();
				response.Success(true);
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while saving the entity changes: {ex.Message}");
				Console.WriteLine(ex.InnerException?.Message);
			}

			return response;
		}




		public async Task<GenericResponseApi<bool>> DeleteVacancy(int Id)
		{
			var response = new GenericResponseApi<bool>();

			var getById = await unitOfWork.GetRepository<Vacancy>().GetById(Id);

			if (getById == null)
			{
				response.Failure("Id not found", 404);
				return response;
			}
			unitOfWork.GetRepository<Vacancy>().Remove(getById);
			await unitOfWork.Commit();

			return response;
		}


		public async Task<GenericResponseApi<bool>> UpdateVacancy(UpdateVacancyDTO updateVacancy)
		{
			var response = new GenericResponseApi<bool>();

			var updateOwnVacancy = await unitOfWork.GetRepository<Vacancy>()
				.GetAsQueryable()
				.FirstOrDefaultAsync(x => x.CompanyId == updateVacancy.companyId && x.Id == updateVacancy.Id);

			if (updateOwnVacancy == null)
			{
				response.Failure("Vacancy not found or you do not have permission to update this vacancy", 404);
				return response;
			}

			var mapping = mapper.Map(updateVacancy, updateOwnVacancy);
			mapping.CompanyId = updateVacancy.companyId;

			unitOfWork.GetRepository<Vacancy>().Update(mapping);
			await unitOfWork.Commit();

			return response;
		}

		public async Task<GenericResponseApi<bool>> DeleteCompanyOwnedVacancy(int Id, int companyId)
		{
			var response = new GenericResponseApi<bool>();

			var deleteOwnVacancy = await unitOfWork.GetRepository<Vacancy>()
				.FirstOrDefaultAsync(v => v.Id == Id && v.CompanyId == companyId);

			if (deleteOwnVacancy == null)
			{
				response.Failure("Id not found or you do not have permission to delete this vacancy", 404);
				return response;
			}

			unitOfWork.GetRepository<Vacancy>().Remove(deleteOwnVacancy);
			await unitOfWork.Commit();
			response.Success(true);


			return response;
		}
	}
}
