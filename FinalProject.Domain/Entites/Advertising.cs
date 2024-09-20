using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entites
{
	public class Advertising : BaseEntity
	{
		public decimal Price {  get; set; }
		public DateTime StartTime { get; set; }
		public DateTime ExpireTime { get; set; } 
		public bool IsPremium {  get; set; }


		public Vacancy Vacancy { get; set; }
		public int VacancyId { get; set;}
	}
}
