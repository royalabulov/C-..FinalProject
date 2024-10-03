using AutoMapper;
using FinalProject.BLL.Models.DTOs.CategoryDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Repositories;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.Extensions.Logging;

namespace FinalProject.BLL.Services.Implementation
{
	public class CategoryService : ICategoryService
	{
		private readonly IMapper mapper;
		private readonly IUnitOfWork unitOfWork;
		private readonly ILogger<CategoryService> logger;

		public CategoryService(IMapper mapper, IUnitOfWork unitOfWork,ILogger<CategoryService> logger)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
			this.logger = logger;
		}

		public async Task<GenericResponseApi<bool>> CreateCategory(CreateCategoryDTO category)
		{
			var response = new GenericResponseApi<bool>();

			if (category == null)
			{
				logger.LogWarning("Attempted to create a category with null data.");
				response.Failure("Category data null", 404);
			}
			var mapping = mapper.Map<Category>(category);
			await unitOfWork.GetRepository<Category>().AddAsync(mapping);
			await unitOfWork.Commit();

			logger.LogInformation("Category created successfully with Id: {CategoryId}", mapping.Id);
			response.Success(true);
			return response;
		}

		public async Task<GenericResponseApi<bool>> DeleteCategory(int id)
		{
			var response = new GenericResponseApi<bool>();


			var getById = await unitOfWork.GetRepository<Category>().GetById(id);
			if (getById == null)
			{
				logger.LogWarning("Attempted to delete a category with Id: {Id} which does not exist.", id);
				response.Failure("Id not found", 404);
				return response;
			}

			unitOfWork.GetRepository<Category>().Remove(getById);
			await unitOfWork.Commit();

			logger.LogInformation("Category deleted successfully with Id: {Id}", id);
			response.Success(true);
			return response;
		}

		public async Task<GenericResponseApi<List<GetAllCategoryDTO>>> GetAllCategories()
		{
			var response = new GenericResponseApi<List<GetAllCategoryDTO>>();


			var categoryEntity = await unitOfWork.GetRepository<Category>().GetAll();
			if (categoryEntity == null)
			{
				logger.LogWarning("No categories found.");
				response.Failure("Category not found", 404);
				return response;
			}
			var mapping = mapper.Map<List<GetAllCategoryDTO>>(categoryEntity);

			response.Success(mapping);
			return response;
		}

		public async Task<GenericResponseApi<bool>> UpdateCategory(UpdateCategoryDTO category)
		{
			var response = new GenericResponseApi<bool>();


			var getById = await unitOfWork.GetRepository<Category>().GetById(category.Id);
			if (getById == null)
			{
				logger.LogWarning("Attempted to update a category with Id: {Id} which does not exist.", category.Id);
				response.Failure("Id not found", 404);
				return response;
			}
			var mapping = mapper.Map(category, getById);
			unitOfWork.GetRepository<Category>().Update(mapping);
			await unitOfWork.Commit();

			logger.LogInformation("Category updated successfully with Id: {Id}", category.Id);
			return response;
		}
	}
}
