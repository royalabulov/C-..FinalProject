using AutoMapper;
using FinalProject.BLL.Models.DTOs.CategoryDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Repositories;

namespace FinalProject.BLL.Services.Implementation
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository categoryRepository;
		private readonly IMapper mapper;

		public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
		{
			this.categoryRepository = categoryRepository;
			this.mapper = mapper;
		}

		public async Task<GenericResponseApi<bool>> CreateCategory(CreateCategoryDTO category)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				if (category == null)
				{
					response.Failure("Category data null", 404);
				}
				var mapping = mapper.Map<Category>(category);
				await categoryRepository.AddAsync(mapping);
				await categoryRepository.Commit();
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while create Category: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> DeleteCategory(int id)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var getById = await categoryRepository.GetById(id);
				if (getById == null)
				{
					response.Failure("Id not found", 404);
					return response;
				}

				categoryRepository.Remove(getById);
				await categoryRepository.Commit();
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while deleting the Category: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseApi<List<GetAllCategoryDTO>>> GetAllCategories()
		{
			var response = new GenericResponseApi<List<GetAllCategoryDTO>>();

			try
			{
				var categoryEntity = await categoryRepository.GetAll();
				if (categoryEntity == null)
				{
					response.Failure("Category not found", 404);
					return response;
				}
				var mapping = mapper.Map<List<GetAllCategoryDTO>>(categoryEntity);
				response.Success(mapping);
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while retrieving Category: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> UpdateCategory(UpdateCategoryDTO category)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var getById = await categoryRepository.GetById(category.Id);
				if (getById == null)
				{
					response.Failure("Id not found", 404);
					return response;
				}
				var mapping = mapper.Map(category, getById);
				categoryRepository.Update(mapping);
				await categoryRepository.Commit();
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while updating the Category: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}
	}
}
