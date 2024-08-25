using FinalProject.BLL.Models.DTOs.JwtDTOs;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace FinalProject.BLL.Services.Implementation
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration configuration;
		private readonly UserManager<AppUser> userManager;

		public TokenService(IConfiguration configuration, UserManager<AppUser> userManager)
		{
			this.configuration = configuration;
			this.userManager = userManager;
		}
		public async Task<GenerateTokenResponse> GenerateToken(AppUser user)
		{
			GenerateTokenResponse token = new GenerateTokenResponse();

			SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["AppSettings:Secret"]));

			int expireMinute = int.Parse(configuration["AppSettings:Expire"]);

			var dateTimeNow = DateTime.UtcNow;
			var expire = dateTimeNow.Add(TimeSpan.FromMinutes(expireMinute));

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Email, user.Email)
			};

			var roles = await userManager.GetRolesAsync(user);
			claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

			JwtSecurityToken jwt = new JwtSecurityToken(
					issuer: configuration["AppSettings:ValidIssuer"],
					audience: configuration["AppSettings:ValidAudience"],
					notBefore: dateTimeNow,
					signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256),
					claims: claims,
					expires: expire

			);


			return await Task.FromResult(new GenerateTokenResponse
			{	
				Token = new JwtSecurityTokenHandler().WriteToken(jwt),
				RefreshToken = CreateRefreshToken(),
				ExpireDate = dateTimeNow.Add(TimeSpan.FromMinutes(expireMinute))
			});
		}


		public string CreateRefreshToken()
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(configuration["AppSettings:RefreshTokenSecret"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var refreshToken = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(refreshToken);
		}
	}
}
