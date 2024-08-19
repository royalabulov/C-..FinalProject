using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.DAL.Context
{

	public class AppDBContext : IdentityDbContext<AppUser, AppRole, int>
	{
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
		{ }

		public DbSet<Category> Categories { get; set; }
		public DbSet<Company> Companys { get; set; }
		public DbSet<Industry> Industry { get; set; }
		public DbSet<Vacancy> Vacancy { get; set; }
	}
}
