using AutoMapper;
using FinalProject.BLL.Models.DTOs.VacancyDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.DAL.Context;
using FinalProject.Domain.Entities;
using FinalProject.Domain.UnitOfWorkInterface;


namespace FinalProject.BLL.Services.Implementation
{
	public class VacancyService : IVacancyService
	{
		private readonly IMapper mapper;
		private readonly IUnitOfWork unitOfWork;
		private readonly AppDBContext context;


		public VacancyService(IMapper mapper, IUnitOfWork unitOfWork,AppDBContext context)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
			this.context = context;
		}
		public async Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetAllVacancy()
		{
			var response = new GenericResponseApi<List<GetAllVacancyDTO>>();

			try
			{

				var vacancyEntity = await unitOfWork.GetRepository<Vacancy>().GetAll();

				if (vacancyEntity == null)
				{
					response.Failure("Vacancy not found", 404);
					return response;
				}

				var mapping = mapper.Map<List<GetAllVacancyDTO>>(vacancyEntity);
				response.Success(mapping);

			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while retrieving Vacancy: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
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

				var category = await unitOfWork.GetRepository<Category>().FirstOrDefaultAsync(c=>c.HeaderName == createVacancy.CategoryName);

				if (category == null)
				{
					response.Failure("Category not found.", 404);
					return response;
				}
				

				var mapping = mapper.Map<Vacancy>(createVacancy);
				mapping.CategoryId = category.Id;
				mapping.CompanyId = createVacancy.CompanyId;
				await unitOfWork.GetRepository<Vacancy>().AddAsync(mapping);
				Console.WriteLine("Before commit...");
				await unitOfWork.Commit();
				Console.WriteLine("After commit - Commit was successful!");

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
