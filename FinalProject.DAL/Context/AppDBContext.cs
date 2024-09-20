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


			modelBuilder.Entity<WishListVacant>()
				.HasOne(wl => wl.VacantProfile)
				.WithMany(vp => vp.WishListVacant)
				.HasForeignKey(wl => wl.VacantProfileId)
				.OnDelete(DeleteBehavior.Cascade);


			modelBuilder.Entity<WishListVacant>()
				.HasOne(wl => wl.Vacancy)
				.WithMany(v => v.WishListVacant)
				.HasForeignKey(wl => wl.VacancyId)
				.OnDelete(DeleteBehavior.Cascade);




			modelBuilder.Entity<Advertising>()
				.Property(a => a.Price)
				.HasColumnType("decimal(18, 2)"); // 18 basamak toplamda, 2 basamak ondalık kısmı


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
