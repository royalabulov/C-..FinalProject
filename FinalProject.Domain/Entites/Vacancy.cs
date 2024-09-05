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
		public DateTime CreateDate { get; set; }
		public DateTime UpdateDate { get; set; }
		public DateTime ExpireDate { get; set; }


		//one to many
		public int CompanyId {  get; set; }
		public Company Company { get; set; }	


		//one to many
		public int CategoryId {  get; set; }
		public Category Category { get; set; }


		//one to one 
		public Advertising Advertising { get; set; }
		public int AdvertisingId {  get; set; }

		//one to one
		public WishListVacancy WishList { get; set; }
    }
}
