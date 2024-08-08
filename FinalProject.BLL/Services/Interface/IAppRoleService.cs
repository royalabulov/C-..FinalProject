using FinalProject.BLL.Models.DTOs.AppRoleDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Interface
{
	public interface IAppRoleService
	{
		Task<GenericResponseApi<List<AppRoleGetDTO>>> GetAllRoles();
		Task<GenericResponseApi<bool>> CreateRole(string roleName);
		Task<GenericResponseApi<bool>> UpdateRole(AppRoleUpdateDTO roleUpdateDTO);
		Task<GenericResponseApi<bool>> RemoveRole(int id);

	}
}
