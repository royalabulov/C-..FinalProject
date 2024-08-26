using FinalProject.BLL.Models.DTOs.CategoryDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Interface
{
	public interface ICategoryService
	{
		Task<GenericResponseApi<List<GetAllCategoryDTO>>> GetAllCategories();
		Task<GenericResponseApi<bool>> CreateCategory(CreateCategoryDTO category);
		Task<GenericResponseApi<bool>> DeleteCategory(int id);
		Task<GenericResponseApi<bool>> UpdateCategory(UpdateCategoryDTO category);
	}
}
