using AutoMapper;
using FinalProject.BLL.Models.DTOs.RegisterDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.DAL.Repositories;
using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Transactions;


namespace FinalProject.BLL.Services.Implementation
{
	public class RegisterService : IRegisterService
	{
		private readonly UserManager<AppUser> userManager;
		private readonly IMapper mapper;
		private readonly ILogger<RegisterService> logger;

		public RegisterService(UserManager<AppUser> userManager, IMapper mapper, ILogger<RegisterService> logger)
		{
			this.userManager = userManager;
			this.mapper = mapper;
			this.logger = logger;
		}



		public async Task<GenericResponseApi<bool>> CreateVacant(UserCreateDTO userCreateDTO)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var existingUser = await userManager.FindByEmailAsync(userCreateDTO.Email);
				if (existingUser != null)
				{
					response.Failure("User with this email already exists.", 400);
					logger.LogWarning("Attempt to create vacant user failed: Email already exists - {Email}", userCreateDTO.Email);
					return response;
				}

				using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
				{
					var mapping = mapper.Map<AppUser>(userCreateDTO);


					var userEntity = await userManager.CreateAsync(mapping, userCreateDTO.Password);
					if (!userEntity.Succeeded)
					{
						response.Failure(userEntity.Errors.Select(e => e.Description).ToList());
						logger.LogError("Failed to create vacant user: {Errors}", string.Join(", ", userEntity.Errors.Select(e => e.Description)));
						return response;
					}

					var addToRoleResult = await userManager.AddToRoleAsync(mapping, "Vacant");
					if (!addToRoleResult.Succeeded)
					{
						await userManager.DeleteAsync(mapping);
						response.Failure("Failed to assign role. User creation has been rolled back.");
						logger.LogError("Failed to assign role to vacant user: {Email}", userCreateDTO.Email);
						return response;
					}

					transaction.Complete();

					response.Success(true);
					logger.LogInformation("Vacant user created successfully: {Email}", userCreateDTO.Email);
				}

			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An unexpected error occurred while creating a vacant user: {Email}", userCreateDTO.Email);
				response.Failure("An unexpected error occurred. Please try again later.", 500);
			}

			return response;
		}

		public async Task<GenericResponseApi<bool>> CreateCompany(CreateCompanyDTO createCompanyDTO)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var existingUser = await userManager.FindByEmailAsync(createCompanyDTO.Email);
				if (existingUser != null)
				{
					response.Failure("User with this email already exists.", 400);
					logger.LogWarning("Attempt to create company user failed: Email already exists - {Email}", createCompanyDTO.Email);
					return response;
				}

				using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
				{
					var mapping = mapper.Map<AppUser>(createCompanyDTO);


					var userEntity = await userManager.CreateAsync(mapping, createCompanyDTO.Password);
					if (!userEntity.Succeeded)
					{
						response.Failure(userEntity.Errors.Select(e => e.Description).ToList());
						logger.LogError("Failed to create company user: {Errors}", string.Join(", ", userEntity.Errors.Select(e => e.Description)));
						return response;
					}


					var addToRoleResult = await userManager.AddToRoleAsync(mapping, "Company");
					if (!addToRoleResult.Succeeded)
					{
						await userManager.DeleteAsync(mapping);
						response.Failure("Failed to assign role. User creation has been rolled back.");
						logger.LogError("Failed to assign role to company user: {Email}", createCompanyDTO.Email);
						return response;
					}

					transaction.Complete();

					response.Success(true);
					logger.LogInformation("Company user created successfully: {Email}", createCompanyDTO.Email);
				}

			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An unexpected error occurred while creating a moderator user: {Email}", createCompanyDTO.Email);
				response.Failure("An unexpected error occurred. Please try again later.", 500);
			}

			return response;
		}

		public async Task<GenericResponseApi<bool>> CreateModerators(ModeratorDTO moderatorDTO)
		{
			var response = new GenericResponseApi<bool>();
			try
			{
				var existingUser = await userManager.FindByEmailAsync(moderatorDTO.Email);
				if (existingUser != null)
				{
					response.Failure("User with this email already exists.", 400);
					logger.LogWarning("Attempt to create company user failed: Email already exists - {Email}", moderatorDTO.Email);
					return response;
				}

				using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
				{
					var mapping = mapper.Map<AppUser>(moderatorDTO);


					var userEntity = await userManager.CreateAsync(mapping, moderatorDTO.Password);
					if (!userEntity.Succeeded)
					{
						response.Failure(userEntity.Errors.Select(e => e.Description).ToList());
						logger.LogError("Failed to create company user: {Errors}", string.Join(", ", userEntity.Errors.Select(e => e.Description)));
						return response;
					}


					var addToRoleResult = await userManager.AddToRoleAsync(mapping, "Moderator");
					if (!addToRoleResult.Succeeded)
					{
						await userManager.DeleteAsync(mapping);
						response.Failure("Failed to assign role. User creation has been rolled back.");
						logger.LogError("Failed to assign role to company user: {Email}", moderatorDTO.Email);
						return response;
					}

					transaction.Complete();

					response.Success(true);
					logger.LogInformation("Company user created successfully: {Email}", moderatorDTO.Email);
				}

			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An unexpected error occurred while creating a moderator user: {Email}", moderatorDTO.Email);
				response.Failure("An unexpected error occurred. Please try again later.", 500);
			}

			return response;
		}


		public async Task<GenericResponseApi<List<AllUserGetDTO>>> GelAllUser()
		{
			var response = new GenericResponseApi<List<AllUserGetDTO>>();

			var userEntity = await userManager.Users.ToListAsync();
			if (userEntity == null)
			{
				response.Failure("User not found", 404);
				logger.LogWarning("Attempt to retrieve users failed: No users found.");
				return response;
			}

			var mapping = mapper.Map<List<AllUserGetDTO>>(userEntity);
			response.Success(mapping);
			logger.LogInformation("Retrieved all users successfully.");

			return response;
		}

		public async Task<GenericResponseApi<bool>> RemoveUser(int id)
		{
			var response = new GenericResponseApi<bool>();

			var getById = await userManager.FindByIdAsync(id.ToString());

			if (getById == null)
			{
				response.Failure("Id not found", 404);
				logger.LogWarning("Attempt to remove user failed: Id not found - {Id}", id);
				return response;
			}

			await userManager.DeleteAsync(getById);
			response.Success(true);
			logger.LogInformation("User removed successfully: {Id}", id);
			return response;
		}

		public async Task<GenericResponseApi<bool>> UpdateUser(UserUpdateDTO userUpdateDTO)
		{
			var response = new GenericResponseApi<bool>();

			var getById = await userManager.FindByIdAsync(userUpdateDTO.Id.ToString());
			if (getById == null)
			{
				response.Failure("Id not found", 404);
				logger.LogWarning("Attempt to update user failed: Id not found - {Id}", userUpdateDTO.Id);
				return response;
			}

			var mapping = mapper.Map(userUpdateDTO, getById);

			if (mapping == null)
			{
				response.Failure("faulty mapping", 400);
				logger.LogWarning("User update failed due to faulty mapping for Id: {Id}", userUpdateDTO.Id);
				return response;
			}
			await userManager.UpdateAsync(mapping);
			response.Success(true);
			logger.LogInformation("User updated successfully: {Id}", userUpdateDTO.Id);
			return response;
		}



		public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenData)
		{
			if (user != null)
			{
				user.RefreshToken = refreshToken;
				user.ExpireTimeRFT = accessTokenData.AddMinutes(15);
				await userManager.UpdateAsync(user);
				logger.LogInformation("Refresh token updated for user: {UserId}", user.Id);
			}
		}

		public async Task<GenericResponseApi<bool>> AssignRoleToUserAsync(string Id, string[] roles)
		{
			var response = new GenericResponseApi<bool>();

			var user = await userManager.FindByIdAsync(Id);
			try
			{
				if (user != null)
				{

					var userRoles = await userManager.GetRolesAsync(user);
					if (userRoles.Count > 0)
					{
						await userManager.RemoveFromRolesAsync(user, userRoles);

					}
					await userManager.AddToRolesAsync(user, roles);

					response.Data = true;
					response.IsSuccess = true;
					response.StatusCode = 200;
					logger.LogInformation("Roles assigned successfully to user: {UserId}", Id);
				}
			}
			catch (Exception ex)
			{
				response.Data = false;
				response.StatusCode = 500;
				await Console.Out.WriteLineAsync(ex.Message);
				logger.LogError(ex, "Error occurred while assigning roles to user: {UserId}", Id);
			}
			return response;
		}

		public async Task<GenericResponseApi<string[]>> GetRolesAsync(int userId)
		{
			var response = new GenericResponseApi<string[]>();

			try
			{
				var user = await userManager.FindByIdAsync(userId.ToString());
				if (user == null)
				{
					response.Data = null;
					response.StatusCode = 404;
					logger.LogWarning("User with ID {UserId} not found.", userId);
					return response;
				}



				var userRoles = await userManager.GetRolesAsync(user);
				if (userRoles == null || !userRoles.Any())
				{

					response.Data = null;
					response.StatusCode = 200;
					response.IsSuccess = true;
					logger.LogInformation("User with ID {UserId} has no roles.", userId);
				}
				else
				{
					response.StatusCode = 200;
					response.Data = userRoles.ToArray();
					response.IsSuccess = true;
					logger.LogInformation("Retrieved roles for user: {UserId}", userId.ToString());

				}

			}
			catch (Exception ex)
			{
				response.Data = null;
				response.StatusCode = 500;
				logger.LogError(ex, "Error occurred while retrieving roles for user: {UserId}", userId.ToString());
			}
			return response;
		}
	}
}
