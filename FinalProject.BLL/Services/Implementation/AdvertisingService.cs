using AutoMapper;
using FinalProject.BLL.Models.DTOs.AdvertisingDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Entities;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinalProject.BLL.Services.Implementation
{
	public class AdvertisingService(IMapper mapper, IUnitOfWork unitOfWork,ILogger<AdvertisingService> logger) : IAdvertisingService
	{


		public async Task<GenericResponseApi<bool>> CreateAdvertising(CreateAdvertisingDTO createAdvertising)
		{
			var response = new GenericResponseApi<bool>();

			logger.LogInformation("Creating advertising for company ID: {CompanyId}", createAdvertising.CompanyId);

			var company = await unitOfWork.GetRepository<Company>().FirstOrDefaultAsync(x => x.Id == createAdvertising.CompanyId);

			if (company == null)
			{
				logger.LogWarning("Company not found for ID: {CompanyId}", createAdvertising.CompanyId);
				response.Failure("Company not found", 404);
				return response;
			}

			if (createAdvertising.Price % 5 != 0)
			{
				logger.LogWarning("Invalid price amount: {Price}", createAdvertising.Price);
				response.Failure("Invalid price amount", 400);
				return response;
			}
			#region
			//pula gore gunu hesabliyacam mes: 10 man gonderibse 5 e bolecem  2 gunluk reklam verecem startdate.addDays(2) gunu gelecem
			//vacancylarin getallinda bunu nezere alacam 
			#endregion

			var days = (int)(createAdvertising.Price / 5);
			var startTime = DateTime.Now;
			var expireTime = startTime.AddDays(days);

			var vacancy = await unitOfWork.GetRepository<Vacancy>()
				.GetAsQueryable()
				.Where(x => x.CompanyId == createAdvertising.CompanyId)
				.ToListAsync();


			foreach (var item in vacancy)
			{
				item.ExpireDate = expireTime;
				item.CreateDate = startTime;
			}


			var mapping = mapper.Map<Advertising>(createAdvertising);
			mapping.StartTime = startTime;
			mapping.ExpireTime = expireTime;



			await unitOfWork.GetRepository<Advertising>().AddAsync(mapping);
			await unitOfWork.Commit();

			logger.LogInformation("Advertising created successfully for company ID: {CompanyId}", createAdvertising.CompanyId);
			response.Success(true);
			return response;

		}



		public async Task<GenericResponseApi<List<GetAllAdvertisingDTO>>> GetAllAdvertising()
		{
			var response = new GenericResponseApi<List<GetAllAdvertisingDTO>>();

			logger.LogInformation("Fetching all advertisements");

			var currentTime = DateTime.Now;

			var advertisements = await unitOfWork.GetRepository<Advertising>().GetAll();

			var mapping = mapper.Map<List<GetAllAdvertisingDTO>>(advertisements);

			foreach (var adTimeLeft in mapping)
			{
				adTimeLeft.TimeLeft = CalculatorTimeLeft(adTimeLeft.ExpireTime, currentTime);
			}

			logger.LogInformation("Fetched {Count} advertisements", mapping.Count);
			response.Success(mapping);
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

	}

}
