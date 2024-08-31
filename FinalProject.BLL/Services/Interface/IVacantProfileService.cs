using FinalProject.BLL.Models.DTOs.AdvertisingDTOs;
using FinalProject.BLL.Models.DTOs.VacantProfileDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Interface
{
	public interface IVacantProfileService
	{
		Task<GenericResponseApi<List<GetAllVacantDTO>>> AllVacant();
		Task<GenericResponseApi<bool>> CreateVacantProfile(CreateVacantProfileDTO createVacantProfile);
		Task<GenericResponseApi<bool>> UpdateVacantProfile(UpdateVacantProfileDTO updateVacantProfile);
		Task<GenericResponseApi<bool>> DeleteVacant(int id);
	}
}
