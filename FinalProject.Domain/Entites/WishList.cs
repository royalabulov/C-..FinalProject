using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entites
{
	public class WishList : BaseEntity
	{
		public string VacancyName { get; set; }


		//one to one
		public int VacancyId {  get; set; }
		public Vacancy Vacancy { get; set;}

		//one to many
		public int VacantProfile {  get; set; }

		public VacantProfile VacantProfil {  get; set; } //e ni qoya bilmedim
	}
}
