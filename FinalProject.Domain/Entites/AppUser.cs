using FinalProject.Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Domain.Entities
{
	public class AppUser : IdentityUser<int>
	{
		//one to one
		
		public Company? Company { get; set; }


		//one to one
		
		public VacantProfile? VacantProfile { get; set; } 
		

	
		public string? RefreshToken { get; set; }
		public DateTime? ExpireTimeRFT { get; set; }

	}
}
