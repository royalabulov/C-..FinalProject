using FinalProject.BLL.Models.DTOs.AdvertisingDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdvertisingController : ControllerBase
    {
		private readonly IAdvertisingService advertisingService;

		public AdvertisingController(IAdvertisingService advertisingService)
        {
			this.advertisingService = advertisingService;
		}

        [HttpGet]
        public async Task<IActionResult> GetVacancyPremium()
        {
            var result = await advertisingService.GetAllAdvertising();
            return StatusCode(result.StatusCode,result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdvertising(CreateAdvertisingDTO createAdvertising)
        {
            var result = await advertisingService.CreateAdvertising(createAdvertising);
            return StatusCode(result.StatusCode, result);
        }
    }
}
