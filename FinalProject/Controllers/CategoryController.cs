﻿using FinalProject.BLL.Models.DTOs.CategoryDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			this.categoryService = categoryService;
		}


		[HttpGet("[action]")]
		public async Task<IActionResult> GetAllCategory()
		{
			var result = await categoryService.GetAllCategories();
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost("[action]")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategory)
		{
			var result = await categoryService.CreateCategory(createCategory);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPut("[action]")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategory)
		{
			var result = await categoryService.UpdateCategory(updateCategory);
			return StatusCode(result.StatusCode, result);
		}

		[HttpDelete("{id:int}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var result = await categoryService.DeleteCategory(id);
			return StatusCode(result.StatusCode, result);
		}

	}
}
