using AutoMapper;
using FinalProject.BLL.Models.DTOs.RegisterDTOs;
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
	public class RegisterService : IRegisterService
	{
		private readonly UserManager<AppUser> userManager;
		private readonly IMapper mapper;

		public RegisterService(UserManager<AppUser> userManager, IMapper mapper)
		{
			this.userManager = userManager;
			this.mapper = mapper;
		}

		public async Task<GenericResponseApi<bool>> Create(UserCreateDTO userCreateDTO)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
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

		public async Task<GenericResponseApi<List<AllUserGetDTO>>> GelAllUser()
		{
			var response = new GenericResponseApi<List<AllUserGetDTO>>();

			try
			{
				var userEntity = await userManager.Users.ToListAsync();
				if (userEntity == null)
				{
					response.Failure("User not found", 404);
					return response;
				}

				var mapping = mapper.Map<List<AllUserGetDTO>>(userEntity);
				response.Success(mapping);

			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while retrieving users: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> RemoveUser(int id)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var getById = await userManager.FindByIdAsync(id.ToString());

				if (getById == null)
				{
					response.Failure("Id not found", 404);
					return response;
				}

				await userManager.DeleteAsync(getById);
				response.Success(true);

			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while deleting the user: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> UpdateUser(UserUpdateDTO userUpdateDTO)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var getById = await userManager.FindByIdAsync(userUpdateDTO.Id.ToString());
				if(getById == null)
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
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while updating the user: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}
	}
}
