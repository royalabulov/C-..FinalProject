using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entites
{
	public class CompanyModerator : BaseEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }


		public int CompanyId { get; set; }
		public int ModeratorId { get; set; }

		public Company Company { get; set; }
		public AppUser Moderator { get; set; }
	}
}
