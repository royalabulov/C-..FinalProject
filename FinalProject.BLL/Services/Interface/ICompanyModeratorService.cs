using FinalProject.BLL.Models.DTOs.ModeratorDTO;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Interface
{
	public interface ICompanyModeratorService
	{
		Task<GenericResponseApi<List<GetAllModeratorDTO>>> GetAllModerators();
		Task<GenericResponseApi<bool>> AddModeratorsCompany(AddModeratorDTO addModeratorDTO);
		Task<GenericResponseApi<List<GetModeratorsByCompanyDTO>>> GetCompanyModerators(int companyId);
		Task<GenericResponseApi<bool>> DeleteModeratorsCompany(int companyId, int moderatorId);
		Task<GenericResponseApi<bool>> DeleteModerator(int moderatorId);

	}
}
