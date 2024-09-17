using FinalProject.Domain.Entites;
using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FinalProject.DAL.Context
{

	public class AppDBContext(DbContextOptions<AppDBContext> options) : IdentityDbContext<AppUser, AppRole, int>(options)
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//modelBuilder.Entity<VacantProfile>()
			//	.HasOne(v => v.AppUser)
			//	.WithOne(u => u.VacantProfile)
			//	.HasForeignKey<VacantProfile>(v => v.AppUserId);

			//modelBuilder.Entity<AppUser>()
			//	.HasOne(u => u.Company)
			//	.WithOne(v => v.AppUser)
			//	.HasForeignKey<AppUser>(u => u.CompanyId);

			//modelBuilder.Entity<Company>()
			//	.HasOne(v => v.AppUser)
			//	.WithOne(u => u.Company)
			//	.HasForeignKey<Company>(v => v.AppUserId);

			//modelBuilder.Entity<WishListVacancy>(entity =>
			//{
			//	entity.ToTable("WishList");

			//	entity.HasIndex(e => e.VacancyId, "IX_WishList_VacancyId").IsUnique();

			//	entity.HasIndex(e => e.VacantProfileId, "IX_WishList_VacantProfilId");

			//	entity.HasOne(d => d.Vacancy).WithOne(p => p.WishList).HasForeignKey<WishListVacancy>(d => d.VacancyId);

			//	entity.HasOne(d => d.VacantProfile).WithMany(p => p.WishList)
			//		.HasForeignKey(d => d.VacantProfileId)
			//		.OnDelete(DeleteBehavior.ClientSetNull);
			//});


			//modelBuilder.Entity<WishListVacant>(entity =>
			//{
			//	entity.ToTable("WishListVacant");

			//	entity.HasIndex(e => e.VacantProfileId, "IX_WishListVacant_VacantProfileId").IsUnique();

			//	entity.HasOne(d => d.VacantProfile).WithMany(p => p.WishListVacant).HasForeignKey(e => e.VacantProfileId);
			//});

			modelBuilder.Entity<Advertising>(entity =>
			{
				entity.ToTable("Advertising");

				entity.HasIndex(e => e.VacancyId, "IX_Advertising_VacancyId").IsUnique();

				entity.HasOne(d => d.Vacancy).WithOne(p => p.Advertising).HasForeignKey<Advertising>(d => d.VacancyId);
			});


			modelBuilder.Entity<VacantProfile>(entity =>
			{
				entity.HasIndex(e => e.AppUserId, "IX_VacantProfiles_AppUserId").IsUnique();

				entity.HasOne(d => d.AppUser).WithOne(p => p.VacantProfile).HasForeignKey<VacantProfile>(d => d.AppUserId);
			});

			modelBuilder.Entity<Company>(entity =>
			{
				entity.HasIndex(e => e.AppUserId, "IX_Companys_AppUserId").IsUnique();

				

				entity.HasOne(d => d.AppUser).WithOne(p => p.Company).HasForeignKey<Company>(d => d.AppUserId);

			});

			modelBuilder.Entity<Vacancy>(entity =>
			{
				entity.ToTable("Vacancy");

				entity.HasIndex(e => e.CategoryId, "IX_Vacancy_CategoryId");

				entity.HasIndex(e => e.CompanyId, "IX_Vacancy_CompanyId");

				entity.HasOne(d => d.Category).WithMany(p => p.Vacancy).HasForeignKey(d => d.CategoryId);

				entity.HasOne(d => d.Company).WithMany(p => p.Vacancies).HasForeignKey(d => d.CompanyId);
			});



		}


		public DbSet<Category> Categories { get; set; }
		public DbSet<Company> Companys { get; set; }
		public DbSet<Vacancy> Vacancy { get; set; }
		public DbSet<Advertising> Advertising { get; set; }
		public DbSet<Subscription> Subscriptions { get; set; }
		public DbSet<VacantProfile> VacantProfiles { get; set; }
		public DbSet<WishListVacancy> WishListVacancies { get; set; }
		public DbSet<WishListVacant> WishListVacants { get; set; }
	}
}
