using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.DTOs.LoginDTOs
{
	public class PasswordResetDTO
	{
		public string Email {  get; set; }
		public string CurrentPassword { get; set; }
		public string NewPassword {  get; set; }

	}
}
