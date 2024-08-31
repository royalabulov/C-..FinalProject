using AutoMapper;
using FinalProject.BLL.Models.DTOs.AdvertisingDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Implementation
{
    public class AdvertisingService(IAdvertisingRepository advertisingRepository,IMapper mapper,IVacanyRepository vacanyRepository) : IAdvertisingService
	{
		
		public async Task<GenericResponseApi<List<GetAllAdvertisingDTO>>> GetAllAdvertising()
		{
			var response = new GenericResponseApi<List<GetAllAdvertisingDTO>>();

			try
			{
				var advertisingEntity = advertisingRepository.GetAsQueryable().Include(x => x.Vacancy).Where(x=>x.Id > 2);
				var a = advertisingEntity.ToList();
			}
			catch (Exception ex)
			{

			}
			return response;
		}
	}
	//unitofwork+,GetAsQuer deyis+,context database duzelt, 
}
