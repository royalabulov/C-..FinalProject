using AutoMapper;
using FinalProject.BLL.Models.DTOs.AppRoleDTOs;
using FinalProject.BLL.Models.DTOs.CategoryDTOs;
using FinalProject.BLL.Models.DTOs.CompanyDTOs;
using FinalProject.BLL.Models.DTOs.RegisterDTOs;
using FinalProject.BLL.Models.DTOs.VacancyDTOs;
using FinalProject.BLL.Models.DTOs.VacantProfileDTOs;
using FinalProject.Domain.Entites;
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

			//Company
			CreateMap<Company,CompanyGetDTO>().ReverseMap();
			CreateMap<Company, CompanyUpdateDTO>().ReverseMap();
			CreateMap<Company,CompanyCreateDTO>().ReverseMap();

			//Vacancy
			CreateMap<Vacancy,GetAllVacancyDTO>().ReverseMap();
			CreateMap<Vacancy, CreateVacancyDTO>().ReverseMap();
			CreateMap<Vacancy, UpdateVacancyDTO>().ReverseMap();

			//Category
			CreateMap<Category,GetAllCategoryDTO>().ReverseMap();
			CreateMap<Category,CreateCategoryDTO>().ReverseMap();
			CreateMap<Category,UpdateCategoryDTO>().ReverseMap();

			CreateMap<VacantProfile,CreateVacantProfileDTO>().ReverseMap();
			CreateMap<VacantProfile, GetAllVacantDTO>().ReverseMap();
		}
	}
}
