using FinalProject.BLL.Models.DTOs.LoginDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly ILoginService loginService;
		private readonly IConfiguration configuration;

		public LoginController(ILoginService loginService,IConfiguration configuration)
        {
			this.loginService = loginService;
			this.configuration = configuration;
		}

		//[HttpPost]
		//public async Task<LoginResponseDTO> Login(LoginCreateDTO loginCreateDTO)
		//{
		//	return await loginService.Login(loginCreateDTO, configuration);
		//}

		[HttpPost]
		public async Task<IActionResult> LogOut()
		{
			var result = await loginService.Logout();
			return StatusCode(result.StatusCode, result);
		}

    }
}
