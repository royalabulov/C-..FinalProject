using FinalProject.BLL.Models.DTOs.JwtDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Implementation
{
	public class TokenService : ITokenService
	{
		public async Task<GenerateTokenResponse> GenerateToken(GenerateTokenRequest request, IConfiguration configuration)
		{
			SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["AppSettings:Secret"]));

			var dateTimeNow = DateTime.Now;
			int expireMinute = int.Parse(configuration["AppSettings:Expire"]);

			JwtSecurityToken jwt = new JwtSecurityToken(
					issuer: configuration["AppSettings:ValidIssuer"],
					audience: configuration["AppSettings:ValidAudience"],
					claims: new List<Claim> {
					new Claim("userName",request.Email),

					},
					notBefore: dateTimeNow,
					expires: dateTimeNow.Add(TimeSpan.FromMinutes(expireMinute)),
					signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
				);

			return await Task.FromResult(new GenerateTokenResponse
			{
				Token = new JwtSecurityTokenHandler().WriteToken(jwt),
				ExpireDate = dateTimeNow.Add(TimeSpan.FromMinutes(expireMinute))
			});
		}
	}
}
