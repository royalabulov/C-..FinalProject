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
			CreateMap<AppUser, UserUpdateDTO>().ReverseMap();
			CreateMap<UserCreateDTO, AppUser>().ForMember(fm => fm.UserName, opt => opt.MapFrom(mf => mf.Email)).ReverseMap();


			//ROLE
			CreateMap<AppRole, AppRoleGetDTO>().ReverseMap();
			CreateMap<AppRole, AppRoleUpdateDTO>().ReverseMap();
		}
	}
}
