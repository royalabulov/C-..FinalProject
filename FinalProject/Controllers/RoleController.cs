using FinalProject.BLL.Models.DTOs.AppRoleDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
	[Route("api")]
	//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly IAppRoleService roleService;

		public RoleController(IAppRoleService roleService)
        {
			this.roleService = roleService;
		}


		[HttpGet("role")]
		public async Task<IActionResult> GetAllRole()
		{
			var result = await roleService.GetAllRoles();
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost("role")]
		public async Task<IActionResult> CreateRole(string roleName)
		{
			var result = await roleService.CreateRole(roleName);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPut("role")]
		public async Task<IActionResult> UpdateRole(AppRoleUpdateDTO role)
		{
			var result = await roleService.UpdateRole(role);
			return StatusCode(result.StatusCode, result);
		}

		[HttpDelete("role{id}")]
		public async Task<IActionResult> DeleteRole(int id)
		{
			var result = await roleService.RemoveRole(id);
			return StatusCode(result.StatusCode, result);
		}
    }
}
