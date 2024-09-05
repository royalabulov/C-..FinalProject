using AutoMapper;
using FinalProject.BLL.Models.DTOs.AppRoleDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entities;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.BLL.Services.Implementation
{
	public class AppRoleService : IAppRoleService
	{
		private readonly RoleManager<AppRole> roleManager;
		private readonly IMapper mapper;


		public AppRoleService(RoleManager<AppRole> roleManager, IMapper mapper)
		{
			this.roleManager = roleManager;
			this.mapper = mapper;
		
		}

		public async Task<GenericResponseApi<List<AppRoleGetDTO>>> GetAllRoles()
		{
			var response = new GenericResponseApi<List<AppRoleGetDTO>>();
			try
			{
				var roleEntity = await roleManager.Roles.ToListAsync();

				if (roleEntity == null)
				{
					response.Failure("Role not found", 404);
					return response;
				}

				var mapping = mapper.Map<List<AppRoleGetDTO>>(roleEntity);
				response.Success(mapping);
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while retrieving Role: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> CreateRole(string roleName)
		{
			var response = new GenericResponseApi<bool>();

			try
			{

				var roleEntity = await roleManager.CreateAsync(new AppRole { Name = roleName });

				if (roleEntity.Succeeded)
				{
					response.Success(true);
				}
				else
				{
					response.Failure(roleEntity.Errors.Select(m => m.Description).ToList(), 400);
				}
                 
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while creating the Role: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}


		public async Task<GenericResponseApi<bool>> RemoveRole(int id)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var getRoleId = await roleManager.FindByIdAsync(id.ToString());

				if (getRoleId == null)
				{
					response.Failure("Id not found", 404);
					return response;
				}
				await roleManager.DeleteAsync(getRoleId);
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while deleting the Role: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> UpdateRole(AppRoleUpdateDTO roleUpdateDTO)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var getRoleId = await roleManager.FindByIdAsync(roleUpdateDTO.Id.ToString());

				if(getRoleId == null)
				{
					response.Failure("Id not found", 404);
					return response;
				}
				var mapping = mapper.Map(roleUpdateDTO, getRoleId);
				await roleManager.UpdateAsync(mapping);
			}catch(Exception ex)
			{
				response.Failure($"An error occurred while updating the Role: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}
	}
}
