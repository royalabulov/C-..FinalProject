using AutoMapper;
using FinalProject.BLL.Mappers;
using FinalProject.BLL.Models.DTOs.VacantProfileDTOs;
using FinalProject.BLL.Models.Exception;
using FinalProject.BLL.Models.Validations.Company;
using FinalProject.BLL.Models.Validations.Login;
using FinalProject.BLL.Models.Validations.Register;
using FinalProject.BLL.Models.Validations.Vacancy;
using FinalProject.BLL.Services.Implementation;
using FinalProject.BLL.Services.Interface;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL
{
    public static class BusinessLogicDependencyInjection
	{
		public static void BusinessLogicInjection(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(IMapperNavigate));

			services.AddScoped<IAppRoleService, AppRoleService>();
			services.AddScoped<IRegisterService, RegisterService>();
			services.AddScoped<ILoginService, LoginService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<ICompanyService, CompanyService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IVacancyService, VacancyService>();
			services.AddScoped<IAdvertisingService, AdvertisingService>();
			services.AddScoped<IVacantProfileService, VacantProfileService>();
			services.AddScoped<IWishListVacantService, WishListVacantService>();
			services.AddScoped<ICompanyModeratorService, CompanyModeratorService>();

			services.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters()
				.AddValidatorsFromAssemblyContaining<RegisterCreateDTOValidator>();

			services.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters()
				.AddValidatorsFromAssemblyContaining<RegisterUpdateDTOValidate>();

			services.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters()
				.AddValidatorsFromAssemblyContaining<CreateVacantProfileDTO>();

			services.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters()
				.AddValidatorsFromAssemblyContaining<UpdateVacantProfileDTO>();

			services.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters()
				.AddValidatorsFromAssemblyContaining<CreateVacancyDTOValidate>();

			services.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters()
				.AddValidatorsFromAssemblyContaining<LoginCreateDTOValidate>();

			services.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters()
				.AddValidatorsFromAssemblyContaining<CompanyCreateDTOValidator>();

			services.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters()
				.AddValidatorsFromAssemblyContaining<CompanyUpdateDTOValidator>();
		}
	}
}
