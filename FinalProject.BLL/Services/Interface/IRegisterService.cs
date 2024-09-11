using FinalProject.BLL.Models.DTOs.RegisterDTOs;
using FinalProject.DAL.Repositories;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.Domain.Entities;
namespace FinalProject.BLL.Services.Interface
{
	public interface IRegisterService
	{
		Task<GenericResponseApi<List<AllUserGetDTO>>> GelAllUser();
		Task<GenericResponseApi<bool>> Create(UserCreateDTO userCreateDTO);
		Task<GenericResponseApi<bool>> UpdateUser(UserUpdateDTO userUpdateDTO);
		Task<GenericResponseApi<bool>> RemoveUser(int id);

		Task UpdateRefreshToken(string refreshToken,AppUser user,DateTime accessTokenData);

		Task<GenericResponseApi<bool>> AssignRoleToUserAsync(string Id, string[] roles);

		Task<GenericResponseApi<string[]>> GetRolesToUserAsync(string userIdOrName);

	}
}
