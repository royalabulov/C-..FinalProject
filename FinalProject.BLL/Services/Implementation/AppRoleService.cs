using AutoMapper;
using FinalProject.BLL.Models.DTOs.AppRoleDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entities;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace FinalProject.BLL.Services.Implementation
{
	public class AppRoleService : IAppRoleService
	{
		private readonly RoleManager<AppRole> roleManager;
		private readonly IMapper mapper;
		private readonly ILogger<AppRoleService> logger;

		public AppRoleService(RoleManager<AppRole> roleManager, IMapper mapper,ILogger<AppRoleService> logger)
		{
			this.roleManager = roleManager;
			this.mapper = mapper;
			this.logger = logger;
		}

		public async Task<GenericResponseApi<List<AppRoleGetDTO>>> GetAllRoles()
		{
			var response = new GenericResponseApi<List<AppRoleGetDTO>>();

			var roleEntity = await roleManager.Roles.ToListAsync();

			if (roleEntity == null)
			{
				response.Failure("Role not found", 404);
				logger.LogWarning("Role not found.");
				return response;
			}

			var mapping = mapper.Map<List<AppRoleGetDTO>>(roleEntity);
			response.Success(mapping);

			logger.LogInformation("Successfully retrieved all roles.");
			return response;
		}

		public async Task<GenericResponseApi<bool>> CreateRole(string roleName)
		{
			var response = new GenericResponseApi<bool>();


			var roleEntity = await roleManager.CreateAsync(new AppRole { Name = roleName });

			if (roleEntity.Succeeded)
			{
				response.Success(true);
				logger.LogInformation($"Role '{roleName}' created successfully.");
			}
			else
			{
				var errors = roleEntity.Errors.Select(m => m.Description).ToList();
				response.Failure(errors, 400);
				logger.LogWarning($"Failed to create role '{roleName}': {string.Join(", ", errors)}");

			}
			return response;
		}


		public async Task<GenericResponseApi<bool>> RemoveRole(int id)
		{
			var response = new GenericResponseApi<bool>();

			var getRoleId = await roleManager.FindByIdAsync(id.ToString());

			if (getRoleId == null)
			{
				response.Failure("Id not found", 404);
				logger.LogWarning($"Role with Id '{id}' not found.");
				return response;
			}
			IdentityResult result = await roleManager.DeleteAsync(getRoleId);
			if (result.Succeeded)
			{
				response.Success(true);
				logger.LogInformation($"Role with Id '{id}' removed successfully.");
				//response.Data = result.Succeeded;
				//response.StatusCode = 200;
			}
			else
			{
				var errors = result.Errors.Select(e => e.Description).ToList();
				response.Failure(errors, 400);
				logger.LogWarning($"Failed to remove role with Id '{id}': {string.Join(", ", errors)}");
			}

			return response;
		}

		public async Task<GenericResponseApi<bool>> UpdateRole(AppRoleUpdateDTO roleUpdateDTO)
		{
			var response = new GenericResponseApi<bool>();

			var getRoleId = await roleManager.FindByIdAsync(roleUpdateDTO.Id.ToString());

			if (getRoleId == null)
			{
				response.Failure("Id not found", 404);
				logger.LogWarning($"Role with Id '{roleUpdateDTO.Id}' not found.");
				return response;
			}
			var mapping = mapper.Map(roleUpdateDTO, getRoleId);
			await roleManager.UpdateAsync(mapping);
			logger.LogInformation($"Role with Id '{roleUpdateDTO.Id}' updated successfully.");

			return response;
		}
	}
}
