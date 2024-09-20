using FinalProject.Domain.Entites;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entities
{
	public class Vacancy : BaseEntity
	{
		public string HeaderName { get; set; }
		public string Responsibilities { get; set; }
		public string Requirements { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.Now;
		public DateTime UpdateDate { get; set; } = DateTime.Now;
		public DateTime ExpireDate { get; set; }
		public bool IsPremium { get; set; } = false;


		public int CompanyId {  get; set; }
		public Company Company { get; set; }	


	
		public int CategoryId {  get; set; }
		public Category? Category { get; set; }


	
		public ICollection<Advertising> Advertising { get; set; }

		public ICollection<WishListVacancy> WishListVacancy { get; set; }
		public ICollection<WishListVacant> WishListVacant { get; set; }

	
    }
}
