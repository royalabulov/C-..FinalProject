using FinalProject.BLL.Models.DTOs.JwtDTOs;
using FinalProject.BLL.Models.DTOs.LoginDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Interface
{
	public interface ILoginService
	{
		Task<GenericResponseApi<GenerateTokenResponse>> Login(LoginCreateDTO login);

		Task<GenericResponseApi<bool>> Logout();
			
		Task<GenericResponseApi<GenerateTokenResponse>> LoginWithRefreshTokenAsync(string refreshToken);

		Task<GenericResponseApi<bool>> PasswordReset(PasswordResetDTO passwordReset);
	}
}
