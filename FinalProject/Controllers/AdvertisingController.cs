using FinalProject.BLL.Models.DTOs.AdvertisingDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    [Route("api/[controller]")]
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
            return StatusCode(result.StatusCode,result);
        }

		[HttpPost("[action]")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Company")]
        public async Task<IActionResult> CreateAdvertising(CreateAdvertisingDTO createAdvertising)
        {
            var result = await advertisingService.CreateAdvertising(createAdvertising);
            return StatusCode(result.StatusCode, result);
        }
    }
}
