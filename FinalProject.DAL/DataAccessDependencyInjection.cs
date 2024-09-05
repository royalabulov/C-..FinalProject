using FinalProject.DAL.Context;
using FinalProject.DAL.Repositories.EntityRepository;
using FinalProject.DAL.UnitOfWorkImplementation;
using FinalProject.Domain.Entites;
using FinalProject.Domain.Repositories;
using FinalProject.Domain.UnitOfWorkInterface;
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
				options.UseSqlServer(configuration.GetConnectionString("FinalProjectDB"));

			});

			services.AddScoped<IVacanyRepository, VacanyRepository>();
			services.AddScoped<ICompanyRepository, CompanyRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IAdvertisingRepository, AdvertisingRepository>();
			services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
			services.AddScoped<IVacantProfileRepository, VacantProfileRepository>();
			services.AddScoped<IWishListVacancyRepository, WishListVacancyRepository>();
			services.AddScoped<IWishListVacantRepository, WishListVacantRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

		}

	}
}
