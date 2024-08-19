using FinalProject.BLL.Models.DTOs.RegisterDTOs;
using FinalProject.DAL.Repositories;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Models.DTOs.AppUserDTOs;

namespace FinalProject.BLL.Services.Interface
{
	public interface IRegisterService
	{
		Task<GenericResponseApi<List<AllUserGetDTO>>> GelAllUser();
		Task<GenericResponseApi<bool>> Create(UserCreateDTO userCreateDTO);
		Task<GenericResponseApi<bool>> UpdateUser(UserUpdateDTO userUpdateDTO);
		Task<GenericResponseApi<bool>> RemoveUser(int id);
	}
}
