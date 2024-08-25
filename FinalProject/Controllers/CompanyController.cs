using FinalProject.BLL.Models.DTOs.CompanyDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class CompanyController : ControllerBase
	{
		private readonly ICompanyService companyService;

		public CompanyController(ICompanyService companyService)
        {
			this.companyService = companyService;
		}


		[HttpGet]
		public async Task<IActionResult> GetAllCompany()
		{
			var result = await companyService.GetAllCompany();
			return StatusCode(result.StatusCode, result);
		}


		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> CreateCompany(CompanyCreateDTO companyCreate)
		{
			var result = await companyService.CreateCompany(companyCreate);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCompany(CompanyUpdateDTO companyUpdate)
		{
			var result = await companyService.UpdateCompany(companyUpdate);
			return StatusCode(result.StatusCode, result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteCompany(int id)
		{
			var result = await companyService.DeleteCompany(id);
			return StatusCode(result.StatusCode, result);
		}

    }
}
