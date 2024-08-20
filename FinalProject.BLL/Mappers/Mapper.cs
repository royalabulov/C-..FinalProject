using AutoMapper;
using FinalProject.BLL.Models.DTOs.AppRoleDTOs;
using FinalProject.BLL.Models.DTOs.RegisterDTOs;
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
			CreateMap<AppUser, AllUserGetDTO>().ReverseMap();
			CreateMap<AppUser, UserUpdateDTO>();
			CreateMap<UserCreateDTO,AppUser>().ForMember(m => m.UserName, op => op.MapFrom(mf => mf.Email)).ReverseMap();//


			//ROLE
			CreateMap<AppRole, AppRoleGetDTO>().ReverseMap();
			CreateMap<AppRole, AppRoleUpdateDTO>().ReverseMap();
		}
	}
}
