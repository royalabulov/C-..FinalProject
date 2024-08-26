using FinalProject.BLL.Models.DTOs.VacancyDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Interface
{
	public interface IVacancyService
	{
		Task<GenericResponseApi<List<GetAllVacancyDTO>>> GetAllVacancy();
		Task<GenericResponseApi<bool>> CreateVacancy(CreateVacancyDTO createVacancy);
		Task<GenericResponseApi<bool>> UpdateVacancy(UpdateVacancyDTO updateVacancy);
		Task<GenericResponseApi<bool>> DeleteVacancy(int Id);
	}
}
