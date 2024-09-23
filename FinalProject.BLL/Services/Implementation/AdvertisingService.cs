using AutoMapper;
using FinalProject.BLL.Models.DTOs.AdvertisingDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Repositories;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;

namespace FinalProject.BLL.Services.Implementation
{
	public class AdvertisingService(IMapper mapper, IUnitOfWork unitOfWork) : IAdvertisingService
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

			if (createAdvertising.Price % 5 != 0)
			{
				response.Failure("Invalid price amount", 400);
				return response;
			}

			//pula gore gunu hesabliyacam mes: 10 man gonderibse 5 e bolecem  2 gunluk reklam verecem startdate.addDays(2) gunu gelecem
			//vacancylarin getallinda bunu nezere alacam 

			var days = (int)(createAdvertising.Price / 5);
			var startTime = DateTime.Now;
			var expireTime = startTime.AddDays(days);


			var mapping = mapper.Map<Advertising>(createAdvertising);
			mapping.StartTime = startTime;
			mapping.ExpireTime = expireTime;


			await unitOfWork.GetRepository<Advertising>().AddAsync(mapping);


			await unitOfWork.Commit();

			return response;

		}



		public async Task<GenericResponseApi<List<GetAllAdvertisingDTO>>> GetAllAdvertising()
		{
			var response = new GenericResponseApi<List<GetAllAdvertisingDTO>>();

			var currentTime = DateTime.Now;

			var advertisements = await unitOfWork.GetRepository<Advertising>().GetAll();

			var mapping = mapper.Map<List<GetAllAdvertisingDTO>>(advertisements);

			foreach (var adTimeLeft in mapping)
			{
				adTimeLeft.TimeLeft = CalculatorTimeLeft(adTimeLeft.ExpireTime, currentTime);
			}


			return response;
		}


		public static string CalculatorTimeLeft(DateTime expireTime, DateTime currentTime)
		{
			var timeSpan = expireTime - currentTime;

			if (timeSpan.TotalDays < 0)
				return "Expired";

			if (timeSpan.TotalDays > 30)
				return $"{Math.Floor(timeSpan.TotalDays / 30)} month(s) left";

			if (timeSpan.TotalDays > 0 && timeSpan.TotalHours > 0)
				return $"{timeSpan.TotalDays},{Math.Floor(timeSpan.TotalHours)} day(s) left";

			return "Less than a day left";
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
