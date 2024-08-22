using FinalProject.BLL.Models.DTOs.JwtDTOs;
using FinalProject.BLL.Models.DTOs.LoginDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.Xml;
using System.Text;


namespace FinalProject.BLL.Services.Implementation
{
	public class LoginService : ILoginService
	{
		private readonly SignInManager<AppUser> signInManager;
		private readonly ITokenService tokenService;
		private readonly IRegisterService registerService;
		private readonly UserManager<AppUser> userManager;

		public LoginService(SignInManager<AppUser> signInManager, ITokenService tokenService, IRegisterService registerService, UserManager<AppUser> userManager)
		{
			this.signInManager = signInManager;
			this.tokenService = tokenService;
			this.registerService = registerService;
			this.userManager = userManager;
		}




		public async Task<GenericResponseApi<bool>> Logout()
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				await signInManager.SignOutAsync();
				response.Success(true, 204);
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while logging out: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}


		public async Task<GenericResponseApi<GenerateTokenResponse>> LoginWithRefreshTokenAsync(string refreshToken)
		{
			var response = new GenericResponseApi<GenerateTokenResponse>();
			AppUser user = await userManager.Users.FirstOrDefaultAsync(rf => rf.RefreshToken == refreshToken);

			try
			{

				if (user != null && user.ExpireTimeRFT > DateTime.UtcNow)
				{
					GenerateTokenResponse token = await tokenService.GenerateToken(user);
					await registerService.UpdateRefreshToken(token.RefreshToken, user, token.ExpireDate.AddMinutes(15));
				}
			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}
			return response;

		}

		public async Task<GenericResponseApi<GenerateTokenResponse>> Login(LoginCreateDTO login, IConfiguration configuration)
		{
			var user = await userManager.FindByNameAsync(login.Email);

			SignInResult result = await signInManager.CheckPasswordSignInAsync(user, login.Password, false);

			if (result.Succeeded)
			{
				GenerateTokenResponse response = await tokenService.GenerateToken(user);

				await registerService.UpdateRefreshToken(response.RefreshToken,user, response.ExpireDate.AddMinutes(15));

				return new() { Data = response, StatusCode = 200 };
			}
			else
			{
				return new() { Data = null, StatusCode = 500 };
			}
			
			
		} //buna bax

		public async Task<GenericResponseApi<bool>> PasswordReset(PasswordResetDTO passwordReset)
		{
			var response = new GenericResponseApi<bool>();

			AppUser user = await userManager.FindByEmailAsync(passwordReset.Email);

			try
			{
				if(user != null)
				{
					var data = userManager.ChangePasswordAsync(user,passwordReset.CurrentPassword,passwordReset.NewPassword);

					if (data != null)
					{
						return response;
					}
				}
			}catch (Exception ex)
			{
				response.Failure($"Password not renewed: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}
	}
}
