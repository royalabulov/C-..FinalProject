using FinalProject.BLL.Models.DTOs.WishListDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace FinalProject.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class WishListVacantController : ControllerBase
	{
		private readonly IWishListVacantService wishListVacant;

		public WishListVacantController(IWishListVacantService wishListVacant)
		{
			this.wishListVacant = wishListVacant;
		}

		[HttpGet]
		public async Task<IActionResult> GetWishListVacant([FromQuery]int vacantProfileId)
		{
			var result = await wishListVacant.GetVacantWishList(vacantProfileId);
			return StatusCode(result.StatusCode, result);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllWishListVacant()
		{
			var result = await wishListVacant.GetAllVacantWishList();
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost]
		public async Task<IActionResult> AddVacantWishList(AddVacantWishListDTO addVacantWishListDTO)
		{
			var result = await wishListVacant.AddVacantWishList(addVacantWishListDTO);
			return StatusCode(result.StatusCode, result);
		}
	}
}
