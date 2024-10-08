﻿using FinalProject.BLL.Models.DTOs.VacancyDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.DAL.Repositories;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Interface
{
	public interface IVacancyService
	{
		//Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetAllVacancy();
		Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetAllVacanciesWithPremium();
		Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetCompanyVacancy(int compnayId);
		Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetCategoryVacancy(int categoryId);
		Task<GenericResponseApi<bool>> CreateVacancy(CreateVacancyDTO createVacancy);
		Task<GenericResponseApi<bool>> UpdateVacancy(UpdateVacancyDTO updateVacancy);
		Task<GenericResponseApi<bool>> DeleteVacancy(int Id);
		Task<GenericResponseApi<bool>> DeleteCompanyOwnedVacancy(int Id, int companyId);
	}
}
