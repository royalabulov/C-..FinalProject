using FinalProject.BLL.Models.DTOs.ModeratorDTO;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
	[Route("api")]
	[ApiController]
	public class CompanyModeratorController : ControllerBase
	{
		private readonly ICompanyModeratorService companyModerator;

		public CompanyModeratorController(ICompanyModeratorService companyModerator)
		{
			this.companyModerator = companyModerator;
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Company")]
		[HttpGet("companyModerator/Id")]
		public async Task<IActionResult> Get(int companyId)
		{
			var result = await companyModerator.GetCompanyModerators(companyId);
			return StatusCode(result.StatusCode, result);

		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Company")]
		[HttpPost("moderator")]
		public async Task<IActionResult> Create(AddModeratorDTO addModeratorDTO)
		{
			var result = await companyModerator.AddModeratorsCompany(addModeratorDTO);
			return StatusCode(result.StatusCode, result);
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Company")]
		[HttpDelete("companyModerator{companyId}/{moderatorId}")]
		public async Task<IActionResult> Delete(int companyId, int moderatorId)
		{
			var result = await companyModerator.DeleteModeratorsCompany(companyId, moderatorId);
			return StatusCode(result.StatusCode, result);
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Company")]
		[HttpGet("allModerator")]
		public async Task<IActionResult> GetAllModerator()
		{
			var result = await companyModerator.GetAllModerators();
			return StatusCode(result.StatusCode, result);
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Company")]
		[HttpDelete("moderatorProfile")]
		public async Task<IActionResult> DeleteModerators(int moderatorId)
		{
			var result = await companyModerator.DeleteModerator(moderatorId);
			return StatusCode(result.StatusCode, result);
		}
	}
}
