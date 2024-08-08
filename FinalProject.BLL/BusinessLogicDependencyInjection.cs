using AutoMapper;
using FinalProject.BLL.Services.Implementation;
using FinalProject.BLL.Services.Interface;
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
			services.AddAutoMapper(typeof(Mapper));

			services.AddScoped<IAppRoleService, AppRoleService>();
			services.AddScoped<IAppUserService, AppUserService>();
			
		}
	}
}
