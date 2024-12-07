using FinalProject.BLL.Models.DTOs.AdvertisingDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
	[Route("api")]
	[ApiController]
	public class AdvertisingController : ControllerBase
	{
		private readonly IAdvertisingService advertisingService;

		public AdvertisingController(IAdvertisingService advertisingService)
		{
			this.advertisingService = advertisingService;
		}

		[HttpGet("premium")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		public async Task<IActionResult> GetVacancyPremium()
		{
			var result = await advertisingService.GetAllAdvertising();
			return StatusCode(result.StatusCode, result);
		}


		[HttpGet("companyPremiumTimeLeft")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Company")]
		public async Task<IActionResult> GetCompanyPremium(int companyId)
		{
			var result = await advertisingService.GetCompanyPremiumTimeLeft(companyId);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost("advertising")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Company")]
		public async Task<IActionResult> CreateAdvertising(CreateAdvertisingDTO createAdvertising)
		{
			var result = await advertisingService.CreateAdvertising(createAdvertising);
			return StatusCode(result.StatusCode, result);
		}


	}
}
