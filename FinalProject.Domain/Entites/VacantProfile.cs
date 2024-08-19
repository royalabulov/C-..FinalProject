using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entites
{
	public class VacantProfile : BaseEntity
	{
		public string Skill { get; set; }
		public string Description { get; set; }
		public string Language { get; set; }
		public string Number { get; set; }
		public string SocialMedia { get; set; }
		public string Experience { get; set; }


		//one to one
		public int AppUserId { get; set; }
		public AppUser AppUser { get; set; }


		//one to many
		public ICollection<WishList> WishList { get; set; }
	}
}
