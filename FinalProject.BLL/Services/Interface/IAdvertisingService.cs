using FinalProject.BLL.Models.DTOs.AdvertisingDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;

namespace FinalProject.BLL.Services.Interface
{
	public interface IAdvertisingService
	{
		Task<GenericResponseApi<List<string>>> GetAllAdvertising();
		Task<GenericResponseApi<bool>> CreateAdvertising(CreateAdvertisingDTO createAdvertising);
		Task<GenericResponseApi<string>> GetCompanyPremiumTimeLeft(int  companyId);

	}
}
