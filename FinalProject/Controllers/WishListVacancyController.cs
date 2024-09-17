using FinalProject.BLL.Models.DTOs.WishListDTOs;
using FinalProject.BLL.Services.Implementation;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WishListVacancyController : ControllerBase
	{
		private readonly IWishListService wishListService;

		public WishListVacancyController(IWishListService wishListService)
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

    }
}
