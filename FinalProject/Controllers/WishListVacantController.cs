using FinalProject.BLL.Models.DTOs.WishListDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace FinalProject.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WishListVacantController : ControllerBase
	{
		private readonly IWishListVacantService wishListVacant;

		public WishListVacantController(IWishListVacantService wishListVacant)
		{
			this.wishListVacant = wishListVacant;
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Vacant")]
		[HttpGet("wishlist/{vacantProfileId}")]
		public async Task<IActionResult> GetWishListVacant(int vacantProfileId)
		{
			var result = await wishListVacant.GetVacantWishList(vacantProfileId);
			return StatusCode(result.StatusCode, result);
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> GetAllWishListVacant()
		{
			var result = await wishListVacant.GetAllVacantWishList();
			return StatusCode(result.StatusCode, result);
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Vacant")]
		[HttpPost]
		public async Task<IActionResult> AddVacantWishList(AddVacantWishListDTO addVacantWishListDTO)
		{
			var result = await wishListVacant.AddVacantWishList(addVacantWishListDTO);
			return StatusCode(result.StatusCode, result);
		}


		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Vacant")]
		[HttpDelete("wishlist/{vacantProfileId}")]
		public async Task<IActionResult> RemoveWishList(int vacantProfileId,[FromQuery] int loggedInUserId)
		{
			var result = await wishListVacant.RemoveVacantWishList(vacantProfileId, loggedInUserId);
			return StatusCode(result.StatusCode, result);
		}
	}
}
