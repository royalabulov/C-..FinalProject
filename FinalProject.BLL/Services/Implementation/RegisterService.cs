using AutoMapper;
using FinalProject.BLL.Models.DTOs.RegisterDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Transactions;


namespace FinalProject.BLL.Services.Implementation
{
	public class RegisterService : IRegisterService
	{
		private readonly UserManager<AppUser> userManager;
		private readonly IMapper mapper;


		public RegisterService(UserManager<AppUser> userManager, IMapper mapper)
		{
			this.userManager = userManager;
			this.mapper = mapper;

		}



		public async Task<GenericResponseApi<bool>> CreateVacant(UserCreateDTO userCreateDTO)
		{
			var response = new GenericResponseApi<bool>();

			var existingUser = await userManager.FindByEmailAsync(userCreateDTO.Email);
			if (existingUser != null)
			{
				response.Failure("User with this email already exists.", 400);
				return response;
			}

			using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				var mapping = mapper.Map<AppUser>(userCreateDTO);


				var userEntity = await userManager.CreateAsync(mapping, userCreateDTO.Password);
				if (!userEntity.Succeeded)
				{
					response.Failure(userEntity.Errors.Select(e => e.Description).ToList());
					return response;
				}

				var addToRoleResult = await userManager.AddToRoleAsync(mapping, "Vacant");
				if (!addToRoleResult.Succeeded)
				{
					await userManager.DeleteAsync(mapping);
					response.Failure("Failed to assign role. User creation has been rolled back.");
					return response;
				}

				transaction.Complete();

				response.Success(true);
			}

			return response;
		}

		public async Task<GenericResponseApi<bool>> CreateCompany(CreateCompanyDTO createCompanyDTO)
		{
			var response = new GenericResponseApi<bool>();

			var existingUser = await userManager.FindByEmailAsync(createCompanyDTO.Email);
			if (existingUser != null)
			{
				response.Failure("User with this email already exists.", 400);
				return response;
			}

			using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				var mapping = mapper.Map<AppUser>(createCompanyDTO);


				var userEntity = await userManager.CreateAsync(mapping, createCompanyDTO.Password);
				if (!userEntity.Succeeded)
				{
					response.Failure(userEntity.Errors.Select(e => e.Description).ToList());
					return response;
				}


				var addToRoleResult = await userManager.AddToRoleAsync(mapping, "Company");
				if (!addToRoleResult.Succeeded)
				{
					await userManager.DeleteAsync(mapping);
					response.Failure("Failed to assign role. User creation has been rolled back.");
					return response;
				}

				transaction.Complete();

				response.Success(true);
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
				return response;
			}

			var mapping = mapper.Map<List<AllUserGetDTO>>(userEntity);
			response.Success(mapping);


			return response;
		}

		public async Task<GenericResponseApi<bool>> RemoveUser(int id)
		{
			var response = new GenericResponseApi<bool>();

			var getById = await userManager.FindByIdAsync(id.ToString());

			if (getById == null)
			{
				response.Failure("Id not found", 404);
				return response;
			}

			await userManager.DeleteAsync(getById);
			response.Success(true);

			return response;
		}

		public async Task<GenericResponseApi<bool>> UpdateUser(UserUpdateDTO userUpdateDTO)
		{
			var response = new GenericResponseApi<bool>();

			var getById = await userManager.FindByIdAsync(userUpdateDTO.Id.ToString());
			if (getById == null)
			{
				response.Failure("Id not found", 404);
				return response;
			}

			var mapping = mapper.Map(userUpdateDTO, getById);

			if (mapping == null)
			{
				response.Failure("faulty mapping", 400);
			}
			await userManager.UpdateAsync(mapping);

			return response;
		}


		public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenData)
		{
			if (user != null)
			{
				user.RefreshToken = refreshToken;
				user.ExpireTimeRFT = accessTokenData.AddMinutes(15);
				await userManager.UpdateAsync(user);
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
					await userManager.RemoveFromRolesAsync(user, userRoles);
					await userManager.AddToRolesAsync(user, roles);

					response.Data = true;
					response.IsSuccess = true;
					response.StatusCode = 200;
				}
			}
			catch (Exception ex)
			{
				response.Data = false;
				response.StatusCode = 500;
				await Console.Out.WriteLineAsync(ex.Message);

			}
			return response;
		}

		public async Task<GenericResponseApi<string[]>> GetRolesToUserAsync(string userIdOrName)
		{
			var response = new GenericResponseApi<string[]>();

			var user = await userManager.FindByIdAsync(userIdOrName);

			try
			{
				if (user != null)
				{
					var userRoles = await userManager.GetRolesAsync(user);

					response.StatusCode = 200;
					response.Data = userRoles.ToArray();
				}

			}
			catch (Exception ex)
			{
				response.Data = null;
				response.StatusCode = 500;
				await Console.Out.WriteLineAsync(ex.Message);
			}
			return response;
		}


	}
}
