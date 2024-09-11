using FinalProject.BLL.Models.DTOs.VacantProfileDTOs;
using FinalProject.BLL.Services.Implementation;
using FinalProject.BLL.Services.Interface;
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


		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await vacantProfileService.AllVacant();
			return StatusCode(result.StatusCode, result);
		}

		


		[HttpPost]
		public async Task<IActionResult> Create(CreateVacantProfileDTO createVacantProfile)
		{
			var result = await vacantProfileService.CreateVacantProfile(createVacantProfile);
			return StatusCode(result.StatusCode, result);
		}

	}
}
