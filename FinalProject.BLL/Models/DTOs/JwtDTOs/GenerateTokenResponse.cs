using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.DTOs.JwtDTOs
{
	public class GenerateTokenResponse
	{
		public string Token { get; set; }
		public DateTime ExpireDate { get; set; }
		public string RefreshToken {  get; set; }
	}
}
