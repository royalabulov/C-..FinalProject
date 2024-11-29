using AutoMapper;
using FinalProject.BLL.Models.DTOs.AdvertisingDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Entities;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;

namespace FinalProject.BLL.Services.Implementation
{
	public class AdvertisingService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<AdvertisingService> logger) : IAdvertisingService
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

			var existingAdvertising = await unitOfWork
				.GetRepository<Advertising>()
				.GetAsQueryable()
				.Where(x => x.CompanyId == createAdvertising.CompanyId)
				.OrderByDescending(x => x.ExpireTime)
				.FirstOrDefaultAsync();

			if (existingAdvertising != null)
			{
				expireTime = existingAdvertising.ExpireTime.AddDays(days);
				logger.LogInformation("Company already has premium time, extending it by {Days} days", days);
			}



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



		public async Task<GenericResponseApi<List<string>>> GetAllAdvertising()
		{
			var response = new GenericResponseApi<List<string>>();

			logger.LogInformation("Fetching all advertisements");

			var currentTime = DateTime.Now;

			var allAdvertisings = await unitOfWork
				.GetRepository<Advertising>()
				.GetAsQueryable()
				.Include(a => a.Company)
				.ToListAsync();

			if (allAdvertisings == null || !allAdvertisings.Any())
			{
				logger.LogInformation("No advertisements found.");
				response.Success(new List<string> { "No active premium time found for any company." });
				return response;
			}
			var premiumTimeInfo = new List<string>();

			// Hər bir şirkət üçün ən son premium vaxtını tapırıq
			var companyGroups = allAdvertisings
				.GroupBy(a => a.Company.Name)  // Şirkət adına görə qruplaşdırırıq
				.ToList();


			foreach (var companyGroup in companyGroups)
			{
				var maxExpireTime = companyGroup
					.Select(a => a.ExpireTime)
					.Max();  // Şirkətə aid olan bütün reklamların ən son expireTime-ı

				var timeLeft = CalculatorTimeLeft(maxExpireTime, currentTime);
				premiumTimeInfo.Add($"{companyGroup.Key}: {timeLeft}"); // Şirkət adı və qalan vaxtı əlavə edirik
			}

			logger.LogInformation("Fetched premium time left for companies: {Companies}", string.Join(", ", premiumTimeInfo));
			response.Success(premiumTimeInfo);
			return response;
		}


		public static string CalculatorTimeLeft(DateTime expireTime, DateTime currentTime)
		{
			var timeSpan = expireTime - currentTime;

			if (timeSpan.TotalSeconds <= 0)
				return "Expired";

			if (timeSpan.TotalDays >= 30)
				return $"{Math.Floor(timeSpan.TotalDays / 30)} month(s) left";

			if (timeSpan.TotalDays >= 1)
				return $"{Math.Floor(timeSpan.TotalDays)} day(s) left";

			if (timeSpan.TotalHours >= 1)
				return $"{Math.Floor(timeSpan.TotalHours)} hour(s) left";

			return "Less than a day left";
		}

		public async Task<GenericResponseApi<string>> GetCompanyPremiumTimeLeft(int companyId)
		{
			var response = new GenericResponseApi<string>();

			logger.LogInformation("Fetching premium time left for company with ID: {CompanyId}", companyId);

			var currentTime = DateTime.Now;


			var companyAdvertisings = await unitOfWork
				.GetRepository<Advertising>()
				.GetAsQueryable()
				.Where(v => v.CompanyId == companyId)
				.ToListAsync();

			if (companyAdvertisings == null || !companyAdvertisings.Any())
			{
				logger.LogInformation("No advertisements found for company with ID: {CompanyId}", companyId);
				response.Success("No active premium time found for this company.");
				return response;
			}

			var maxExpireTime = companyAdvertisings
				.Select(x => x.ExpireTime)
				.Max();

			var timeLeft = CalculatorTimeLeft(maxExpireTime, currentTime);

			logger.LogInformation("Premium time left for company with ID: {CompanyId} is {TimeLeft}", companyId, timeLeft);

			response.Success(timeLeft);
			return response;
		}
	}

}
