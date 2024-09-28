using FinalProject.BLL.Models.DTOs.AppRoleDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FinalProject.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly IAppRoleService roleService;

		public RoleController(IAppRoleService roleService)
        {
			this.roleService = roleService;
		}


		[HttpGet]
		public async Task<IActionResult> GetAllRole()
		{
			var result = await roleService.GetAllRoles();
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateRole(string roleName)
		{
			var result = await roleService.CreateRole(roleName);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateRole(AppRoleUpdateDTO role)
		{
			var result = await roleService.UpdateRole(role);
			return StatusCode(result.StatusCode, result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteRole(int id)
		{
			var result = await roleService.RemoveRole(id);
			return StatusCode(result.StatusCode, result);
		}
    }
}
