using AutoMapper;
using FinalProject.BLL.Models.DTOs.AdvertisingDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Repositories;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.BLL.Services.Implementation
{
	public class AdvertisingService(IAdvertisingRepository advertisingRepository, IMapper mapper, IUnitOfWork unitOfWork) : IAdvertisingService
	{

		public async Task<GenericResponseApi<bool>> CreateAdvertising(CreateAdvertisingDTO createAdvertising)
		{
			var response = new GenericResponseApi<bool>();

			var vacancy = await unitOfWork.GetRepository<Vacancy>().FirstOrDefaultAsync(x => x.Id == createAdvertising.VacancyId);

			if (vacancy == null)
			{
				response.Failure("Vacancy not found", 404);
				return response;
			}
			//pula gore gunu hesabliyacam mes: 10 man gonderibse 5 e bolecem  2 gunluk reklam verecem startdate.addDays(2) gunu gelecem
			//vacancylarin getallinda bunu nezere alacam 
			//vacancy.IsPremium = true;
			//vacancy.UpdateDate = DateTime.Now;
			//unitOfWork.GetRepository<Vacancy>().Update(vacancy);

			var mapping = mapper.Map<Advertising>(createAdvertising);

			await unitOfWork.GetRepository<Advertising>().AddAsync(mapping);
			

			await unitOfWork.Commit();

			return response;
			
		}
		



		public async Task<GenericResponseApi<List<GetAllAdvertisingDTO>>> GetAllAdvertising()
		{
			var response = new GenericResponseApi<List<GetAllAdvertisingDTO>>();

			var advertisements = await unitOfWork.GetRepository<Advertising>().GetAll();

			foreach(var ads in advertisements)
			{
				if(DateTime.Now > ads.ExpireTime)
				{
					var vacancy = await unitOfWork.GetRepository<Vacancy>().GetById(ads.VacancyId);

					vacancy.IsPremium = false;
					vacancy.UpdateDate = DateTime.Now;
				    unitOfWork.GetRepository<Vacancy>().Update(vacancy);

					ads.IsPremium = false;
				    unitOfWork.GetRepository<Advertising>().Update(ads);
				}
			}
			await unitOfWork.Commit();

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
				if (getById == null)
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
