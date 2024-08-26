using AutoMapper;
using Azure;
using FinalProject.BLL.Models.DTOs.VacancyDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Implementation
{
	public class VacancyService : IVacancyService
	{
		private readonly IVacanyRepository vacanyRepository;
		private readonly IMapper mapper;

		public VacancyService(IVacanyRepository vacanyRepository, IMapper mapper)
		{
			this.vacanyRepository = vacanyRepository;
			this.mapper = mapper;
		}
		public async Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetAllVacancy()
		{
			var response = new GenericResponseApi<List<GetAllVacancyDTO>>();

			try
			{
				var vacancyEntity = await vacanyRepository.GetAll();

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
				var mapping = mapper.Map<Vacancy>(createVacancy);

				await vacanyRepository.AddAsync(mapping);
				await vacanyRepository.Commit();
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
				var getById = await vacanyRepository.GetById(Id);

				if (getById == null)
				{
					response.Failure("Id not found", 404);
					return response;
				}
				vacanyRepository.Remove(getById);
				await vacanyRepository.Commit();
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
				var getById = await vacanyRepository.GetById(updateVacancy.Id);
				if(getById == null)
				{
					response.Failure("Id not found", 404);
					return response;
				}
				var mapping = mapper.Map(updateVacancy, getById);
				vacanyRepository.Update(mapping);
				await vacanyRepository.Commit();
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
