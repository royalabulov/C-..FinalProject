﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.DTOs.CompanyDTOs
{
	public class CompanyGetDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string About { get; set; }
		public string Address { get; set; }
		public string ContactNumber {  get; set; }
	
	}
} 
