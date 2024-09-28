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

			modelBuilder.Entity<WishListVacant>().ToTable(nameof(WishListVacant));
			modelBuilder.Entity<WishListVacant>().Ignore(s => s.Id);
			modelBuilder.Entity<WishListVacant>().HasKey(sc => new { sc.VacancyId, sc.VacantProfileId });


			modelBuilder.Entity<Advertising>()
				.Property(a => a.Price)
				.HasColumnType("decimal(18, 2)");

			modelBuilder.Entity<Advertising>()
		   .HasOne(a => a.Company)
		   .WithMany(c => c.Advertising)
		   .HasForeignKey(a => a.CompanyId);

			modelBuilder.Entity<Vacancy>()
				.HasOne(v => v.Company)
				.WithMany(c => c.Vacancies)
				.HasForeignKey(v => v.CompanyId);

			modelBuilder.Entity<Vacancy>()
				.HasOne(v => v.Advertising)
				.WithMany(a => a.Vacancy)
				.HasForeignKey(v => v.AdvertisingId);


			modelBuilder.Entity<Company>()
				.HasMany(c => c.Advertising)
				.WithOne(a => a.Company)
				.HasForeignKey(a => a.CompanyId)
				.OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<VacantProfile>(entity =>
				{
					entity.HasIndex(e => e.AppUserId, "IX_VacantProfiles_AppUserId").IsUnique();

					entity.HasOne(d => d.AppUser).WithOne(p => p.VacantProfile).HasForeignKey<VacantProfile>(d => d.AppUserId);
					entity.HasMany(d => d.WishListVacant).WithOne(p => p.VacantProfile).HasForeignKey(d => d.VacantProfileId).OnDelete(DeleteBehavior.Restrict);
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
				entity.HasMany(d => d.WishListVacant).WithOne(p => p.Vacancy).HasForeignKey(d => d.VacancyId).OnDelete(DeleteBehavior.Restrict);
			});



		}


		public DbSet<Category> Categories { get; set; }
		public DbSet<Company> Companys { get; set; }
		public DbSet<Vacancy> Vacancy { get; set; }
		public DbSet<Advertising> Advertising { get; set; }
		public DbSet<Subscription> Subscriptions { get; set; }
		public DbSet<VacantProfile> VacantProfiles { get; set; }
		//public DbSet<WishListVacancy> WishListVacancies { get; set; }
		public DbSet<WishListVacant> WishListVacants { get; set; }
	}
}
