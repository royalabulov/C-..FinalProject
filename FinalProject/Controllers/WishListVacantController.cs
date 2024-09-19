using FinalProject.BLL.Models.DTOs.WishListDTOs;
using FinalProject.BLL.Services.Interface;
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

		[HttpGet]
		public async Task<IActionResult> GetAllWishListVacant()
		{
			var result = await wishListVacant.GetVacantWishList();
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
