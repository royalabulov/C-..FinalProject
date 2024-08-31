using FinalProject.BLL.Services.Interface;
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await advertisingService.GetAllAdvertising();
            return Ok(result);
        } 
    }
}
