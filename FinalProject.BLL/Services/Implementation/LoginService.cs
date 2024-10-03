 using FinalProject.BLL.Models.DTOs.JwtDTOs;
using FinalProject.BLL.Models.DTOs.LoginDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
		private readonly ILogger<LoginService> logger;

		public LoginService(SignInManager<AppUser> signInManager, ITokenService tokenService, IRegisterService registerService, UserManager<AppUser> userManager,ILogger<LoginService> logger)
		{
			this.signInManager = signInManager;
			this.tokenService = tokenService;
			this.registerService = registerService;
			this.userManager = userManager;
			this.logger = logger;
		}

		public async Task<GenericResponseApi<bool>> Logout()
		{
			var response = new GenericResponseApi<bool>();

			var user = signInManager.Context.User;
			if (user?.Identity?.IsAuthenticated != true)
			{
				response.Failure("No active session found. User is not logged in.", 400);
				logger.LogWarning("Logout attempted with no active session.");
				return response;
			}

			await signInManager.SignOutAsync();
			response.Success(true);

			logger.LogInformation("User logged out successfully.");
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

					response.Success(token);
					logger.LogInformation("Successfully logged in using refresh token.");
				}
			}
			catch (Exception ex)
			{
				response.Failure($"Error during login with refresh token: {ex.Message}");
				logger.LogError(ex, "Error occurred during login with refresh token.");
			}
			return response;

		}

		public async Task<GenericResponseApi<GenerateTokenResponse>> Login(LoginCreateDTO login)
		{
			var response = new GenericResponseApi<GenerateTokenResponse>();
			var user = await userManager.FindByNameAsync(login.Email);

			SignInResult result = await signInManager.CheckPasswordSignInAsync(user, login.Password, false);

			if (result.Succeeded)
			{
				GenerateTokenResponse tokenResponse = await tokenService.GenerateToken(user);

				await registerService.UpdateRefreshToken(tokenResponse.RefreshToken, user, tokenResponse.ExpireDate.AddMinutes(15));

				response.Success(tokenResponse);
				logger.LogInformation("User logged in successfully.");
				return response;
			}
			else
			{
				response.Failure("Invalid login attempt.", 401);
				logger.LogWarning("Failed login attempt for user: {Email}", login.Email); // Uyarı logu
				return response;
			}
		} 


		public async Task<GenericResponseApi<bool>> PasswordReset(PasswordResetDTO passwordReset)
		{
			var response = new GenericResponseApi<bool>();

			AppUser user = await userManager.FindByEmailAsync(passwordReset.Email);

			try
			{
				if (user != null)
				{
					var data = userManager.ChangePasswordAsync(user, passwordReset.CurrentPassword, passwordReset.NewPassword);

					if (data != null)
					{
						logger.LogInformation("Password changed successfully for user: {Email}", passwordReset.Email);
						return response;
					}
				}
				else
				{
					logger.LogWarning("Password reset attempt for non-existing user: {Email}", passwordReset.Email);
				}
			}
			catch (Exception ex)
			{
				response.Failure($"Password not renewed: {ex.Message}");
				logger.LogError(ex, "Error occurred during password reset for user: {Email}", passwordReset.Email);
			}
			return response;
		}
	}
}
