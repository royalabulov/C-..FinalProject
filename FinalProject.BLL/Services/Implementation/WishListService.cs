using AutoMapper;
using FinalProject.BLL.Models.DTOs.WishListDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Repositories;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Implementation
{
	public class WishListService : IWishListService
	{
		private readonly IWishListRepository wishListRepository;
		private readonly IMapper mapper;
		private readonly IUnitOfWork unitOfWork;

		public WishListService(IWishListRepository wishListRepository,IMapper mapper,IUnitOfWork unitOfWork)
        {
			this.wishListRepository = wishListRepository;
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
		}
        public async Task<GenericResponseApi<List<GetAllWishListDTO>>> GetAllWishList()
		{
			var response = new GenericResponseApi<List<GetAllWishListDTO>>();

			try
			{
				var wishListEntity = wishListRepository.GetAsQueryable().Include(x => x.VacantProfile).Include(x => x.Vacancy).ToListAsync();
				if (wishListEntity == null)
				{
					response.Failure("WishList data null", 400);
					return response;
				}
				var mapping = mapper.Map<List<GetAllWishListDTO>>(wishListEntity);
				response.Success(mapping);
			}
			catch (Exception ex)
			{
				response.Failure($"An error occurred while retrieving wishlist: {ex.Message}");
				Console.WriteLine(ex.Message);
			}
			return response;
		}
	}
}
