using AutoMapper;
using FinalProject.BLL.Models.DTOs.AppUserDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Implementation
{
	public class AppUserService : IAppUserService
	{
		private readonly UserManager<AppUser> userManager;
		private readonly IMapper mapper;

		public AppUserService(UserManager<AppUser> userManager, IMapper mapper)
		{
			this.userManager = userManager;
			this.mapper = mapper;
		}

		public async Task<GenericResponseApi<List<AppUserGetDTO>>> GetAllUsers()
		{
			var response = new GenericResponseApi<List<AppUserGetDTO>>();

			try
			{
				var userEntity = await userManager.Users.ToListAsync();
				if (userEntity == null)
				{
					response.Failure("User not found", 404);
					return response;
				}

				var mapping = mapper.Map<List<AppUserGetDTO>>(userEntity);
				response.Success(mapping);


			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while retrieving users: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> CreateUser(AppUserCreateDTO userCreateDTO)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				if (userCreateDTO == null)
				{
					response.Failure("User data null", 404);

				}
				var mapping = mapper.Map<AppUser>(userCreateDTO);
				var userEntity = await userManager.CreateAsync(mapping, userCreateDTO.Password);

				if (userEntity.Succeeded)
				{
					response.Success(true);
				}
				else
				{
					response.Failure(userEntity.Errors.Select(m => m.Description).ToList());
				}
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while creating the user: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> RemoveUser(int id)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var getUserId = await userManager.FindByIdAsync(id.ToString());
				if(getUserId == null)
				{
					response.Failure("Id not found", 404);
					return response;
				}
				await userManager.DeleteAsync(getUserId);
				response.Success(true);	
			}catch(Exception ex)
			{
				response.Failure($"An error occurred while deleting the user: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> UpdateUser(AppUserUpdateDTO userUpdateDTO)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var getUserId = await userManager.FindByIdAsync(userUpdateDTO.Id.ToString());
				if(getUserId == null)
				{
					response.Failure("Id not found", 404);
					return response;
				}
				var mapping = mapper.Map(userUpdateDTO,getUserId);
				if(mapping == null)
				{
					response.Failure("faulty mapping", 400);
				}
				await userManager.UpdateAsync(mapping);

			}catch(Exception ex)
			{
				response.Failure($"An error occurred while updating the user: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}


	}
}
