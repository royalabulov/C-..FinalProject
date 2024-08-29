﻿using AutoMapper;
using FinalProject.BLL.Mappers;
using FinalProject.BLL.Models.Validations.Register;
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


			services.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters()
				.AddValidatorsFromAssemblyContaining<RegisterCreateDTOValidator>();

			services.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters()
				.AddValidatorsFromAssemblyContaining<RegisterUpdateDTOValidate>();

		}
	}
}
