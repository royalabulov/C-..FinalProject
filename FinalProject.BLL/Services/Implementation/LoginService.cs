using Azure.Core;
using FinalProject.BLL.Models.DTOs.JwtDTOs;
using FinalProject.BLL.Models.DTOs.LoginDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Implementation
{
	public class LoginService : ILoginService
	{
		private readonly SignInManager<AppUser> signInManager;
		private readonly ITokenService tokenService;

		public LoginService(SignInManager<AppUser> signInManager,ITokenService tokenService)
        {
			this.signInManager = signInManager;
			this.tokenService = tokenService;
		}

        public async Task<LoginResponseDTO> Login(LoginCreateDTO login, IConfiguration configuration)
		{
			LoginResponseDTO response = new LoginResponseDTO();

			try
			{
				if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
				{
					//response.Failure("Email or Password is null");	
					throw new ArgumentNullException(nameof(login));
				}

				var loginEntity = await signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
				if (loginEntity.Succeeded)
				{

					var generatedTokenInformation = await tokenService.GenerateToken(new GenerateTokenRequest { Email = login.Email },
						configuration);

					response.Token = generatedTokenInformation.Token;
					response.ExpireDate = generatedTokenInformation.ExpireDate;
				}
				else
				{
					//response.Failure("Invalid login attempt.");
					throw new ArgumentNullException(nameof(login));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
				throw;
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> Logout()
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				await signInManager.SignOutAsync();
				response.Success(true, 204);
			}catch(Exception ex)
			{
				response.Failure($"An error occurred while logging out: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}
	}
}
