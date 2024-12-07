using FinalProject.BLL.Models.DTOs.LoginDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.Validations.Login
{
	public class LoginCreateDTOValidate : AbstractValidator<LoginCreateDTO>
	{
		public LoginCreateDTOValidate()
		{
			RuleFor(x => x.Email)
		   .NotEmpty().WithMessage("Email field is required.")
		   .EmailAddress().WithMessage("Invalid email format.");

			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Password field is required.")
				.MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
				.Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
				.Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
				.Matches("[0-9]").WithMessage("Password must contain at least one number.");
		}
	}
}
