﻿using AutoMapper;
using FinalProject.BLL.Models.DTOs.AdvertisingDTOs;
using FinalProject.BLL.Models.DTOs.AppRoleDTOs;
using FinalProject.BLL.Models.DTOs.CategoryDTOs;
using FinalProject.BLL.Models.DTOs.CompanyDTOs;
using FinalProject.BLL.Models.DTOs.RegisterDTOs;
using FinalProject.BLL.Models.DTOs.VacancyDTOs;
using FinalProject.BLL.Models.DTOs.VacantProfileDTOs;
using FinalProject.BLL.Models.DTOs.WishListDTOs;
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
			CreateMap<CreateCompanyDTO, AppUser>().ForMember(fm => fm.UserName, opt => opt.MapFrom(mf => mf.Email)).ReverseMap();
			//ROLE
			CreateMap<AppRole, AppRoleGetDTO>().ReverseMap();
			CreateMap<AppRole, AppRoleUpdateDTO>().ReverseMap();

			//Company
			CreateMap<Company, CompanyGetDTO>().ReverseMap();
			CreateMap<Company, CompanyUpdateDTO>().ReverseMap();
			CreateMap<Company, CompanyCreateDTO>().ReverseMap();

			//Vacancy
			CreateMap<Vacancy, GetAllVacancyDTO>().ReverseMap();
			CreateMap<Vacancy, CreateVacancyDTO>().ReverseMap();
			CreateMap<Vacancy, UpdateVacancyDTO>().ReverseMap();

			//Category
			CreateMap<Category, GetAllCategoryDTO>().ReverseMap();
			CreateMap<Category, CreateCategoryDTO>().ReverseMap();
			CreateMap<Category, UpdateCategoryDTO>().ReverseMap();

			CreateMap<VacantProfile, CreateVacantProfileDTO>().ReverseMap();
			CreateMap<VacantProfile, GetAllVacantDTO>().ReverseMap();

			CreateMap<WishListVacancy, GetAllVacancyWishListDTO>()
				.ForMember(a => a.VacancyName, opt => opt.MapFrom(x => x.Vacancy.ToList()));

			CreateMap<WishListVacant, GetAllVacancyDTO>()
				.ForMember(a => a.Id, opt => opt.MapFrom(src => src.Vacancy.Id))
				.ForMember(a => a.HeaderName, opt => opt.MapFrom(src => src.Vacancy.HeaderName))
				.ForMember(a => a.Responsibilities, opt => opt.MapFrom(src => src.Vacancy.Responsibilities))
				.ForMember(a => a.Requirements, opt => opt.MapFrom(src => src.Vacancy.Requirements));

			CreateMap<WishListVacant, AddVacantWishListDTO>().ForMember(a => a.VacantProfileId, opt => opt.MapFrom(src => src.VacantProfileId))
				.ForMember(a => a.VacancyId, opt => opt.MapFrom(src => src.VacancyId)).ReverseMap();


			CreateMap<CreateAdvertisingDTO, Advertising>()
		       .ForMember(dest => dest.IsPremium, opt => opt.MapFrom(src => true));

		}
	}
}
