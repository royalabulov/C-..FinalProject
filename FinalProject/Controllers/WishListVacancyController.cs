using FinalProject.BLL.Models.DTOs.WishListDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WishListVacancyController : ControllerBase
	{
		private readonly IWishListVacancyService wishListService;

		public WishListVacancyController(IWishListVacancyService wishListService)
        {
			this.wishListService = wishListService;
		}


		[HttpGet]
		public async Task<IActionResult> GetVacancyWishList()
		{
			var result = await wishListService.GetVacancyWishList();
			return StatusCode(result.StatusCode, result);
		}



		[HttpPost]
		public async Task<IActionResult> AddVacancyToWishList([FromBody]AddVacancyWishListDTO addVacancyWishListDTO)
		{
			var result = await wishListService.AddVacancyWishList(addVacancyWishListDTO);
			return StatusCode(result.StatusCode, result);
		}

		[HttpDelete]
		public async Task<IActionResult> RemoveWishList(int wishListId, int vacancyId)
		{
			var result = await wishListService.RemoveVacancyWishList(wishListId,vacancyId);
			return StatusCode(result.StatusCode, result);
		}

	}
}
