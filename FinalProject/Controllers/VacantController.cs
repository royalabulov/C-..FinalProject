using FinalProject.BLL.Models.DTOs.VacantProfileDTOs;
using FinalProject.BLL.Services.Implementation;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VacantController : ControllerBase
	{
		private readonly IVacantProfileService vacantProfileService;

		public VacantController(IVacantProfileService vacantProfileService)
		{
			this.vacantProfileService = vacantProfileService;
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await vacantProfileService.AllVacant();
			return StatusCode(result.StatusCode, result);
		}



		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Vacant")]
		[HttpPost]
		public async Task<IActionResult> Create(CreateVacantProfileDTO createVacantProfile)
		{
			var result = await vacantProfileService.CreateVacantProfile(createVacantProfile);
			return StatusCode(result.StatusCode, result);
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Vacant")]
		[HttpPut]
		public async Task<IActionResult> UpdateVacant(UpdateVacantProfileDTO updateVacantProfile)
		{
			var result = await vacantProfileService.UpdateVacantProfile(updateVacantProfile);
			return StatusCode(result.StatusCode, result);
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await vacantProfileService.DeleteVacant(id);
			return StatusCode(result.StatusCode, result);
		}
	}
}
