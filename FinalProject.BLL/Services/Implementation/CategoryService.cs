using AutoMapper;
using FinalProject.BLL.Models.DTOs.CategoryDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Repositories;
using FinalProject.Domain.UnitOfWorkInterface;

namespace FinalProject.BLL.Services.Implementation
{
	public class CategoryService : ICategoryService
	{
		private readonly IMapper mapper;
		private readonly IUnitOfWork unitOfWork;

		public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
		}

		public async Task<GenericResponseApi<bool>> CreateCategory(CreateCategoryDTO category)
		{
			var response = new GenericResponseApi<bool>();

			if (category == null)
			{
				response.Failure("Category data null", 404);
			}
			var mapping = mapper.Map<Category>(category);
			await unitOfWork.GetRepository<Category>().AddAsync(mapping);
			await unitOfWork.Commit();

			response.Success(true);
			return response;
		}

		public async Task<GenericResponseApi<bool>> DeleteCategory(int id)
		{
			var response = new GenericResponseApi<bool>();


			var getById = await unitOfWork.GetRepository<Category>().GetById(id);
			if (getById == null)
			{
				response.Failure("Id not found", 404);
				return response;
			}

			unitOfWork.GetRepository<Category>().Remove(getById);
			await unitOfWork.Commit();

			response.Success(true);
			return response;
		}

		public async Task<GenericResponseApi<List<GetAllCategoryDTO>>> GetAllCategories()
		{
			var response = new GenericResponseApi<List<GetAllCategoryDTO>>();


			var categoryEntity = await unitOfWork.GetRepository<Category>().GetAll();
			if (categoryEntity == null)
			{
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
				response.Failure("Id not found", 404);
				return response;
			}
			var mapping = mapper.Map(category, getById);
			unitOfWork.GetRepository<Category>().Update(mapping);
			await unitOfWork.Commit();

			return response;
		}
	}
}
