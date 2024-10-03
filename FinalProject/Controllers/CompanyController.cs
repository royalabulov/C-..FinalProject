using FinalProject.BLL.Models.DTOs.CompanyDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FinalProject.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CompanyController : ControllerBase
	{
		private readonly ICompanyService companyService;

		public CompanyController(ICompanyService companyService)
		{
			this.companyService = companyService;
		}


		[HttpGet("[action]")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		public async Task<IActionResult> GetAllCompany()
		{
			var result = await companyService.GetAllCompany();
			return StatusCode(result.StatusCode, result);
		}


		[HttpPost("[action]")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Company")]
		public async Task<IActionResult> CreateCompany(CompanyCreateDTO companyCreate)
		{
			var result = await companyService.CreateCompany(companyCreate);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPut("[action]")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Company")]
		public async Task<IActionResult> UpdateCompany(CompanyUpdateDTO companyUpdate)
		{
			var result = await companyService.UpdateCompany(companyUpdate);
			return StatusCode(result.StatusCode, result);
		}

		[HttpDelete("{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		public async Task<IActionResult> DeleteCompany(int id)
		{
			var result = await companyService.DeleteCompany(id);
			return StatusCode(result.StatusCode, result);
		}

	}
}
