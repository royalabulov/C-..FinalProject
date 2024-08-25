using FinalProject.BLL.Models.DTOs.CompanyDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.DAL.Repositories;
using FinalProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Interface
{
	public interface ICompanyService
	{
		Task<GenericResponseApi<List<CompanyGetDTO>>> GetAllCompany();
		Task<GenericResponseApi<bool>> CreateCompany(CompanyCreateDTO companyCreate);
		Task<GenericResponseApi<bool>> UpdateCompany(CompanyUpdateDTO companyUpdate);
		Task<GenericResponseApi<bool>> DeleteCompany(int id);
	}
}
