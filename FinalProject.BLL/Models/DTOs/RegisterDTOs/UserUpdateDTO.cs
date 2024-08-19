using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.DTOs.RegisterDTOs
{
	public class UserUpdateDTO
	{
		public int Id { get; set; }
		public string FirsName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Address { get; set; }
	}
}
