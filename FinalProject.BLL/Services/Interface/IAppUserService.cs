using FinalProject.BLL.Models.DTOs.AppUserDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Interface
{
	public interface IAppUserService
	{
		Task<GenericResponseApi<List<AppUserGetDTO>>> GetAllUsers();
		Task<GenericResponseApi<bool>> CreateUser(AppUserCreateDTO userCreateDTO);
		Task<GenericResponseApi<bool>> UpdateUser(AppUserUpdateDTO userUpdateDTO);
		Task<GenericResponseApi<bool>> RemoveUser(int id);

	}
}
