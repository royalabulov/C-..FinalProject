using FinalProject.BLL.Models.DTOs.JwtDTOs;
using FinalProject.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Interface
{
	public interface ITokenService
	{
		Task<GenerateTokenResponse> GenerateToken(AppUser user);
		string CreateRefreshToken();
	}
}
