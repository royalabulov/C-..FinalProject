using FinalProject.BLL.Models.DTOs.CategoryDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			this.categoryService = categoryService;
		}


		[HttpGet]
		public async Task<IActionResult> GetAllCategory()
		{
			var result = await categoryService.GetAllCategories();
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategory)
		{
			var result = await categoryService.CreateCategory(createCategory);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategory)
		{
			var result = await categoryService.UpdateCategory(updateCategory);
			return StatusCode(result.StatusCode, result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var result = await categoryService.DeleteCategory(id);
			return StatusCode(result.StatusCode, result);
		}

	}
}
