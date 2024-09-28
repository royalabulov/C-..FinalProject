﻿using FinalProject.BLL.Models.DTOs.VacancyDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
		private readonly IVacancyService vacancyService;

		public VacancyController(IVacancyService vacancyService)
        {
			this.vacancyService = vacancyService;
		}

        [HttpGet]
        public async Task<IActionResult> GetAllVacancies()
        {
            var result = await vacancyService.GetAllVacanciesWithPremium();
            return StatusCode(result.StatusCode, result);
        }

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Company")]
		[HttpGet]
        public async Task<IActionResult> GetCompanyVacancy(int compnayId)
        {
            var result = await vacancyService.GetCompanyVacancy(compnayId);
            return StatusCode(result.StatusCode, result);
        }

		[HttpGet]
		public async Task<IActionResult> GetCategoryVacancy(int categoryId)
		{
			var result = await vacancyService.GetCategoryVacancy(categoryId);
			return StatusCode(result.StatusCode, result);
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Company")]
		[HttpPost]
        public async Task<IActionResult> CreateVacancy(CreateVacancyDTO createVacancy)
        {
            var result = await vacancyService.CreateVacancy(createVacancy);
            return StatusCode(result.StatusCode, result);
        }

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Company")]
		[HttpPut]
        public async Task<IActionResult> UpdateVacancy(UpdateVacancyDTO updateVacancy)
        {
            var result = await vacancyService.UpdateVacancy(updateVacancy);
            return StatusCode(result.StatusCode, result);
        }

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		[HttpDelete]
        public async Task<IActionResult> DeleteVacancy(int id)
        {
            var result = await vacancyService.DeleteVacancy(id);
            return StatusCode(result.StatusCode, result);
        }

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Company")]
		[HttpDelete]
		public async Task<IActionResult> DeleteOwnVacancy(int id, int copmanyId)
		{
			var result = await vacancyService.DeleteCompanyOwnedVacancy(id, copmanyId);
			return StatusCode(result.StatusCode, result);
		}
	}
}
