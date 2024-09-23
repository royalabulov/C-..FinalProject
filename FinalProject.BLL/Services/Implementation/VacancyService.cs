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


			var vacancies = await unitOfWork.GetRepository<Vacancy>().GetAsQueryable()
				.Include(x => x.Company)
				.OrderByDescending(v => v.CreateDate)
				.ToListAsync();

			var premiumVacancy = new List<Vacancy>();
			var regularVacancy = new List<Vacancy>();


			foreach (var vacancy in vacancies)
			{
				if (await IsVacancyPremium(vacancy.Id))
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

			response.Data = mapping;

			return response;
		}

		public async Task<bool> IsVacancyPremium(int Id)
		{
			var currentDate = DateTime.Now;

			var activeAds = await unitOfWork.GetRepository<Advertising>()
				.FirstOrDefaultAsync(x => x.VacancyId == Id && x.StartTime <= currentDate && x.ExpireTime >= currentDate);

			return activeAds != null;
		}

		public async Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetCompanyVacancy(int compnayId)
		{
			var response = new GenericResponseApi<List<GetAllVacancyDTO>>();

			var vacancies = await unitOfWork.GetRepository<Vacancy>().GetAsQueryable()
				.Where(x=>x.CompanyId == compnayId)
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

				var category = await unitOfWork.GetRepository<Category>().FirstOrDefaultAsync(c => c.HeaderName == createVacancy.CategoryName);

				if (category == null)
				{
					response.Failure("Category not found.", 404);
					return response;
				}


				var mapping = mapper.Map<Vacancy>(createVacancy);
				mapping.CategoryId = category.Id;
				mapping.CompanyId = createVacancy.CompanyId;
				await unitOfWork.GetRepository<Vacancy>().AddAsync(mapping);
				await unitOfWork.Commit();
				response.Success(true);
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while create Vacancy: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}




		public async Task<GenericResponseApi<bool>> DeleteVacancy(int Id)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var getById = await unitOfWork.GetRepository<Vacancy>().GetById(Id);

				if (getById == null)
				{
					response.Failure("Id not found", 404);
					return response;
				}
				unitOfWork.GetRepository<Vacancy>().Remove(getById);
				await unitOfWork.Commit();
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while deleting the Vacancy: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}


		public async Task<GenericResponseApi<bool>> UpdateVacancy(UpdateVacancyDTO updateVacancy)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var getById = await unitOfWork.GetRepository<Vacancy>().GetById(updateVacancy.Id);
				if (getById == null)
				{
					response.Failure("Id not found", 404);
					return response;
				}
				var mapping = mapper.Map(updateVacancy, getById);
				unitOfWork.GetRepository<Vacancy>().Update(mapping);
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
