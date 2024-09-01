using AutoMapper;
using FinalProject.BLL.Models.DTOs.AdvertisingDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Repositories;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Implementation
{
	public class AdvertisingService(IAdvertisingRepository advertisingRepository, IMapper mapper,IUnitOfWork unitOfWork) : IAdvertisingService
	{
		public async Task<GenericResponseApi<bool>> CreateAdvertising(CreateAdvertisingDTO createAdvertising)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				if (advertisingRepository == null)
				{
					response.Failure("Advertising data null", 404);
				}
				var mapping = mapper.Map<Advertising>(createAdvertising);
				await unitOfWork.GetRepository<Advertising>().AddAsync(mapping);

				await unitOfWork.Commit();
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while create Category: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseApi<List<GetAllAdvertisingDTO>>> GetAllAdvertising()
		{
			var response = new GenericResponseApi<List<GetAllAdvertisingDTO>>();
			try
			{
				var advertisingEntity = advertisingRepository.GetAsQueryable().Include(x => x.Vacancy).Where(x => x.Id > 1).ToListAsync();
				if (advertisingEntity != null)
				{
					response.Failure("Advertising not found", 404);
					return response;
				}
				var mapping = mapper.Map<List<GetAllAdvertisingDTO>>(advertisingEntity);
				response.Success(mapping);
				await unitOfWork.Commit();
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while retrieving advertising: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> RemoveAdvertising(int id)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var getById = await unitOfWork.GetRepository<Advertising>().GetById(id);
				if (getById == null)
				{
					response.Failure("Id not found", 404);
					return response;
				}
				unitOfWork.GetRepository<Advertising>().Remove(getById);
				await unitOfWork.Commit();

			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while deleting the Advertising: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}


		public async Task<GenericResponseApi<bool>> UpdateAdvertising(UpdateAdvertisingDTO updateAdvertising)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var getById = await unitOfWork.GetRepository<Advertising>().GetById(updateAdvertising.Id);	
				if(getById == null)
				{
					response.Failure("Id not found", 404);
					return response;
				}
				var mapping = mapper.Map(updateAdvertising, getById);
				unitOfWork.GetRepository<Advertising>().Update(mapping);
				await unitOfWork.Commit();   
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while updating the Category: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}
	}

}
