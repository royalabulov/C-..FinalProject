using FinalProject.DAL.Context;
using FinalProject.DAL.Repositories;
using FinalProject.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL
{
	public static class DataAccessDependencyInjection
	{
		public static void DataAccessDependencyInjectionMethod(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddDbContext<AppDBContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("Default"));

			});

			services.AddScoped<IVacanyRepository, VacanyRepository>();
			services.AddScoped<IIndustryRepository, IndustryRepository>();
			services.AddScoped<ICompanyRepository, CompanyRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();

		}

	}
}
