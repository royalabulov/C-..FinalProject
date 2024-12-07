using AutoMapper;
using FinalProject.BLL.Models.DTOs.ModeratorDTO;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Entities;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinalProject.BLL.Services.Implementation
{
	public class CompanyModeratorService : ICompanyModeratorService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;
		private readonly ILogger logger;
		private readonly UserManager<AppUser> userManager;


		public CompanyModeratorService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CompanyModerator> logger, UserManager<AppUser> userManager)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
			this.logger = logger;
			this.userManager = userManager;
		}
		public async Task<GenericResponseApi<bool>> AddModeratorsCompany(AddModeratorDTO addModeratorDTO)
		{
			var response = new GenericResponseApi<bool>();
			try
			{

				var company = await unitOfWork.GetRepository<Company>().GetById(addModeratorDTO.companyId);
				if (company == null)
				{
					response.Failure("Company not found", 404);
					return response;
				}


				var moderator = await userManager.FindByIdAsync(addModeratorDTO.moderatorId.ToString());
				if (moderator == null)
				{
					response.Failure("Moderator not found", 404);
					return response;
				}

				var companyModerator = new CompanyModerator
				{
					FirstName = addModeratorDTO.FirstName,
					LastName = addModeratorDTO.LastName,
					CompanyId = addModeratorDTO.companyId,
					ModeratorId = addModeratorDTO.moderatorId,
				};
				await unitOfWork.GetRepository<CompanyModerator>().AddAsync(companyModerator);
				await unitOfWork.Commit();
				response.Success(true);

			}
			catch (Exception ex)
			{
				response.Failure("err");
				return response;
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> DeleteModerator(int moderatorId)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var moderator = await userManager.FindByIdAsync(moderatorId.ToString());

				if (moderator == null)
				{
					response.Failure("Moderator not found.", 404);
					return response;
				}

				
				var result = await userManager.DeleteAsync(moderator);

				if (!result.Succeeded)
				{
					response.Failure("Failed to delete moderator.", 500);
					return response;
				}

				response.Success(true);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred while deleting moderator with ID: {ModeratorId}", moderatorId);

				throw new ApplicationException("An unexpected error occurred while deleting the moderator. Please try again later.", ex);
			}

			return response;
		}

		public async Task<GenericResponseApi<bool>> DeleteModeratorsCompany(int companyId, int moderatorId)
		{
			var response = new GenericResponseApi<bool>();
			try
			{
				var companyModerator = await unitOfWork.GetRepository<CompanyModerator>()
					.FirstOrDefaultAsync(cm => cm.CompanyId == companyId && cm.ModeratorId == moderatorId);

				if (companyModerator == null)
				{
					response.Failure("The specified moderator is not assigned to this company.", 404);
					return response;
				}

				unitOfWork.GetRepository<CompanyModerator>().Remove(companyModerator);
				await unitOfWork.Commit();

				response.Success(true);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred while deleting the moderator from the company.");
				response.Failure("An unexpected error occurred.", 500);
			}

			return response;
		}

		public async Task<GenericResponseApi<List<GetAllModeratorDTO>>> GetAllModerators()
		{
			var response = new GenericResponseApi<List<GetAllModeratorDTO>>();

			try
			{
				var moderator = await userManager.GetUsersInRoleAsync("Moderator");

				if(!moderator.Any())
				{
					response.Failure("No moderators found.", 404);
					return response;
				}

				var moderatorDtos = moderator.Select(m => new GetAllModeratorDTO
				{
					Id = m.Id,
				    Email = m.Email

				}).ToList();

				response.Success(moderatorDtos);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred while fetching all moderators.");
				throw new ApplicationException("An unexpected error occurred while retrieving moderators. Please try again later.", ex);
			}
			return response;
		}

		public async Task<GenericResponseApi<List<GetModeratorsByCompanyDTO>>> GetCompanyModerators(int companyId)
		{
			var response = new GenericResponseApi<List<GetModeratorsByCompanyDTO>>();
			try
			{
				var company = await unitOfWork.GetRepository<Company>().GetById(companyId);

				if (company == null)
				{
					response.Failure("Company not found.", 404);
					return response;
				}

				var moderator = await unitOfWork.GetRepository<CompanyModerator>()
					.GetAsQueryable()
					.Include(x => x.Moderator)
					.Where(x => x.CompanyId == companyId)
					.ToListAsync();

				if (!moderator.Any())
				{
					response.Failure("No moderators found for this company.", 404);
					return response;
				}

				var moderatorDto = moderator.Select(x => new GetModeratorsByCompanyDTO
				{
					Id = x.Moderator.Id,
					FirstName = x.FirstName,
					LastName = x.LastName,

				}).ToList();

				response.Success(moderatorDto);

			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred while fetching moderators for company ID: {CompanyId}", companyId);

				throw new ApplicationException("An unexpected error occurred while retrieving moderators. Please try again later.", ex);
			}
			return response;
		}
	}
}
