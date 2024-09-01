using FinalProject.BLL.Models.DTOs.AdvertisingDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Interface
{
	public interface IAdvertisingService
	{
		Task<GenericResponseApi<List<GetAllAdvertisingDTO>>> GetAllAdvertising();
		Task<GenericResponseApi<bool>> CreateAdvertising(CreateAdvertisingDTO createAdvertising);
		Task<GenericResponseApi<bool>> UpdateAdvertising(UpdateAdvertisingDTO updateAdvertising);
		Task<GenericResponseApi<bool>> RemoveAdvertising(int id);
	}
}
