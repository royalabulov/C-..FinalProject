using AutoMapper;
using FinalProject.BLL.Models.DTOs.AppRoleDTOs;
using FinalProject.BLL.Models.DTOs.AppUserDTOs;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Mappers
{
	public class Mapper : Profile
	{
		public Mapper()
		{
			//USER
			CreateMap<AppUser,AppUserGetDTO>().ReverseMap();
			CreateMap<AppUser,AppUserUpdateDTO>();
			CreateMap<AppUser, AppUserCreateDTO>().ForMember(m=>m.Email,op=>op.MapFrom(mf=>mf.UserName)).ReverseMap();//


			//ROLE
			CreateMap<AppRole,AppRoleGetDTO>().ReverseMap();
			CreateMap<AppRole, AppRoleUpdateDTO>().ReverseMap();
		}
	}
}
