using FinalProject.Domain.Entites;
using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.DAL.Context
{

	public class AppDBContext : IdentityDbContext<AppUser, AppRole, int>
	{
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
		{ }

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

			modelBuilder.Entity<WishList>(entity =>
			{
				entity.ToTable("WishList");

				entity.HasIndex(e => e.VacancyId, "IX_WishList_VacancyId").IsUnique();

				entity.HasIndex(e => e.VacantProfileId, "IX_WishList_VacantProfilId");

				entity.HasOne(d => d.Vacancy).WithOne(p => p.WishList).HasForeignKey<WishList>(d => d.VacancyId);

				entity.HasOne(d => d.VacantProfile).WithMany(p => p.WishList)
					.HasForeignKey(d => d.VacantProfileId)
					.OnDelete(DeleteBehavior.ClientSetNull);
			});

		}


		public DbSet<Category> Categories { get; set; }
		public DbSet<Company> Companys { get; set; }
		public DbSet<Vacancy> Vacancy { get; set; }
		public DbSet<Advertising> Advertising { get; set; }
		public DbSet<Subscription> Subscriptions { get; set; }
		public DbSet<VacantProfile> VacantProfiles { get; set; }
		public DbSet<WishList> WishList { get; set; }
	}
}
