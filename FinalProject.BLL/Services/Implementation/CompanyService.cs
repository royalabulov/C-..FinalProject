using AutoMapper;
using FinalProject.BLL.Models.DTOs.CompanyDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.DAL.UnitOfWorkImplementation;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Repositories;
using FinalProject.Domain.UnitOfWorkInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Implementation
{
	public class CompanyService : ICompanyService
	{
		private readonly IMapper mapper;
		private readonly IUnitOfWork unitOfWork;

		public CompanyService(IMapper mapper,IUnitOfWork unitOfWork)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
		}

		public async Task<GenericResponseApi<List<CompanyGetDTO>>> GetAllCompany()
		{
			var response = new GenericResponseApi<List<CompanyGetDTO>>();

			try
			{
				var companyEntity = await unitOfWork.GetRepository<Company>().GetAll();
				if (companyEntity == null)
				{
					response.Failure("Company not found", 404);
					return response;
				}
				var mapping = mapper.Map<List<CompanyGetDTO>>(companyEntity);
				response.Success(mapping);
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while retrieving Companies: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> CreateCompany(CompanyCreateDTO companyCreate)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				if (companyCreate == null)
				{
					response.Failure("Company data null", 404);
				}
				var mapping = mapper.Map<Company>(companyCreate);
				await unitOfWork.GetRepository<Company>().AddAsync(mapping);
				await unitOfWork.Commit();

			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while create Companies: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> DeleteCompany(int id)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var getById = await unitOfWork.GetRepository<Company>().GetById(id);
				if (getById == null)
				{
					response.Failure("Id not found", 404);
					return response;
				}
				unitOfWork.GetRepository<Company>().Remove(getById);
				await unitOfWork.Commit();

			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while deleting the Company: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}


		public async Task<GenericResponseApi<bool>> UpdateCompany(CompanyUpdateDTO companyUpdate)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var getById = await unitOfWork.GetRepository<Company>().GetById(companyUpdate.Id);
				if(getById == null)
				{
					response.Failure("Id not found", 404);
					return response;
				}
				var mapping = mapper.Map(companyUpdate, getById);
				unitOfWork.GetRepository<Company>().Update(mapping);
				await unitOfWork.Commit();
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while updating the Company: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}
	}
}
