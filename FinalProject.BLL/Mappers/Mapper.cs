using AutoMapper;
using FinalProject.BLL.Models.DTOs.AdvertisingDTOs;
using FinalProject.BLL.Models.DTOs.AppRoleDTOs;
using FinalProject.BLL.Models.DTOs.CategoryDTOs;
using FinalProject.BLL.Models.DTOs.CompanyDTOs;
using FinalProject.BLL.Models.DTOs.RegisterDTOs;
using FinalProject.BLL.Models.DTOs.VacancyDTOs;
using FinalProject.BLL.Models.DTOs.VacantProfileDTOs;
using FinalProject.BLL.Models.DTOs.WishListDTOs;
using FinalProject.BLL.Services.Implementation;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Entities;

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

			//Vacant
			CreateMap<VacantProfile, CreateVacantProfileDTO>().ReverseMap();
			CreateMap<VacantProfile, GetAllVacantDTO>().ReverseMap();
			CreateMap<VacantProfile,UpdateVacantProfileDTO>().ReverseMap();


			//WishListVacant
			CreateMap<WishListVacant, GetAllVacancyDTO>()
				.ForMember(ws => ws.Id, opt => opt.MapFrom(src => src.Vacancy.Id))
				.ForMember(ws => ws.HeaderName, opt => opt.MapFrom(src => src.Vacancy.HeaderName))
				.ForMember(ws => ws.Responsibilities, opt => opt.MapFrom(src => src.Vacancy.Responsibilities))
				.ForMember(ws => ws.Requirements, opt => opt.MapFrom(src => src.Vacancy.Requirements))
				.ForMember(ws => ws.CompanyName, opt => opt.MapFrom(src => src.Vacancy.Company.Name));

			CreateMap<WishListVacant, AddVacantWishListDTO>().ForMember(ws => ws.VacantProfileId, opt => opt.MapFrom(src => src.VacantProfileId))
				.ForMember(ws => ws.VacancyId, opt => opt.MapFrom(src => src.VacancyId)).ReverseMap();


			//Advertising
			CreateMap<CreateAdvertisingDTO, Advertising>()
			   .ForMember(dest => dest.StartTime, opt => opt.Ignore())
			   .ForMember(dest => dest.ExpireTime, opt => opt.Ignore())
			   .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

			CreateMap<Advertising, GetAllAdvertisingDTO>()
			   .ForMember(dest => dest.TimeLeft,
			   opt => opt.MapFrom(src => AdvertisingService.CalculatorTimeLeft(src.ExpireTime, DateTime.Now)));



			CreateMap<WishListVacancy, GetAllVacancyWishListDTO>()
				.ForMember(a => a.VacancyName, opt => opt.MapFrom(x => x.Vacancy.ToList()));
		}
	}
}
