using AutoMapper;
using FinalProject.BLL.Models.DTOs.VacancyDTOs;
using FinalProject.BLL.Models.DTOs.WishListDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Entities;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace FinalProject.BLL.Services.Implementation
{
	public class WishListVacantService : IWishListVacantService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;
		private readonly ILogger<WishListVacantService> logger;

		public WishListVacantService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<WishListVacantService> logger)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
			this.logger = logger;
		}



		public async Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetVacantWishList(int vacantProfileId)
		{
			var response = new GenericResponseApi<List<GetAllVacancyDTO>>();

			logger.LogInformation($"GetVacantWishList method started for VacantProfileId: {vacantProfileId}");

			var wishList = await unitOfWork.GetRepository<WishListVacant>()
				.GetAsQueryable()
				.Where(x => x.VacantProfileId == vacantProfileId)
				.Include(v => v.Vacancy).ThenInclude(x => x.Company)
				.ToListAsync();

			if (wishList == null || !wishList.Any())
			{
				logger.LogWarning($"No wish lists found for VacantProfileId: {vacantProfileId}");
				response.Failure("No vacant profiles found", 404);
				return response;
			}

			var mapping = mapper.Map<List<GetAllVacancyDTO>>(wishList.Select(x => x.Vacancy).ToList().Distinct().ToList());

			response.Success(mapping);

			logger.LogInformation($"GetVacantWishList method completed successfully for VacantProfileId: {vacantProfileId}");
			return response;
		}

		public async Task<GenericResponseApi<List<GetAllVacantWishListDTO>>> GetAllVacantWishList()
		{
			var response = new GenericResponseApi<List<GetAllVacantWishListDTO>>();

			logger.LogInformation("GetAllVacantWishList method started.");

			var wishList = await unitOfWork.GetRepository<WishListVacant>()
				.GetAsQueryable()
				.Include(x => x.VacantProfile)
				.Include(v => v.Vacancy)
				.ThenInclude(x => x.Company)
				.ToListAsync();

			if (wishList == null || !wishList.Any())
			{
				logger.LogInformation("GetAllVacantWishList method started.");
				response.Failure("No vacant profiles found", 404);
				return response;
			}

			var result = wishList
				.GroupBy(x => x.VacantProfile)
				.Select(group => new GetAllVacantWishListDTO
				{
					VacantProfileId = group.Key.Id,
					VacantProfileName = group.FirstOrDefault().VacantProfile.FirstName,
					VacancyDetails = group
					.GroupBy(x => x.VacancyId)
					.Select(x => new VacancyDetailsDTO
					{
						VacancyId = x.Key,
						VacancyName = x.FirstOrDefault().Vacancy.HeaderName,
						CompanyName = x.FirstOrDefault().Vacancy.Company.Name
					}).ToList()

				}).ToList();


			

			logger.LogInformation("GetAllVacantWishList method completed successfully.");
			response.Success(result);
			return response;
		}

		public async Task<GenericResponseApi<bool>> AddVacantWishList(AddVacantWishListDTO addVacant)
		{
			var response = new GenericResponseApi<bool>();

			logger.LogInformation("AddVacantWishList method started.");

			var vacant = await unitOfWork.GetRepository<VacantProfile>()
				.FirstOrDefaultAsync(x => x.Id == addVacant.VacantProfileId);

			var vacancy = await unitOfWork.GetRepository<Vacancy>()
				.FirstOrDefaultAsync(x => x.Id == addVacant.VacancyId);

			if (vacancy == null || vacant == null)
			{
				logger.LogWarning("Vacant or Vacancy not found.");
				response.Failure("Vacant or Vacancy not found", 404);
				return response;
			}

			var wishListProfile = mapper.Map<WishListVacant>(addVacant);

			await unitOfWork.GetRepository<WishListVacant>().AddAsync(wishListProfile);

			await unitOfWork.Commit();
			response.Success(true);

			logger.LogInformation("AddVacantWishList method completed successfully.");
			return response;


		}

		public async Task<GenericResponseApi<bool>> RemoveVacantWishList(int vacantProfileId, int vacancyId)
		{
			var response = new GenericResponseApi<bool>();

			logger.LogInformation($"RemoveVacantWishList method started for VacantProfileId: {vacantProfileId} and UserId: {vacancyId}");

			var wishList = await unitOfWork.GetRepository<WishListVacant>()
				.GetAsQueryable()
				.Include(x => x.VacantProfile)
				.FirstOrDefaultAsync(x => x.VacantProfileId == vacantProfileId && x.VacancyId == vacancyId);

			if (wishList == null)
			{
				logger.LogWarning($"No matching wish list found for VacantProfileId: {vacantProfileId} and UserId: {vacancyId}");
				response.Failure("No matching wish list found for the current user", 404);
				return response;
			}

			unitOfWork.GetRepository<WishListVacant>().Remove(wishList);
			await unitOfWork.Commit();
			response.Success(true);
			logger.LogInformation($"RemoveVacantWishList method completed successfully for VacantProfileId: {vacantProfileId} and UserId: {vacancyId}");
			return response;
		}
	}
}
