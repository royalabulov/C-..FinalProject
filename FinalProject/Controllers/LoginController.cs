using FinalProject.BLL.Models.DTOs.LoginDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly ILoginService loginService;
		

		public LoginController(ILoginService loginService)
        {
			this.loginService = loginService;
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginCreateDTO loginCreateDTO)
		{
			var result = await loginService.Login(loginCreateDTO);
			return StatusCode(result.StatusCode,result);
		}

		[HttpPost]
		public async Task<IActionResult> LogOut()
		{
			var result = await loginService.Logout();
			return StatusCode(result.StatusCode, result);
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> LoginWithRefreshToken(string refreshToken)
		{
			var result = await loginService.LoginWithRefreshTokenAsync(refreshToken);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost]
		public async Task<IActionResult> PasswordReset(PasswordResetDTO passwordResetDTO)
		{
			var result = await loginService.PasswordReset(passwordResetDTO);
			return StatusCode(result.StatusCode, result);
		}

    }
}
