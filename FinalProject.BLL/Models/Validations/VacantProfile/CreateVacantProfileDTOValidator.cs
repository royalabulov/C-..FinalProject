using FinalProject.BLL.Models.DTOs.CompanyDTOs;
using FinalProject.BLL.Models.DTOs.VacantProfileDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.Validations.VacantProfile
{
	public class CreateVacantProfileDTOValidator : AbstractValidator<CreateVacantProfileDTO>
	{
        public CreateVacantProfileDTOValidator()
        {
			RuleFor(a => a.FirstName)
			   .NotEmpty().WithMessage("Username cannot be empty.")
			   .Length(3, 40).WithMessage("Username must be between 3 and 40 characters.")
			   .WithName("FirstName");

			RuleFor(a => a.LastName)
				.NotEmpty().WithMessage("LastName cannot be empty.")
				.Length(3, 40).WithMessage("LastName must be between 3 and 40 characters.")
				.WithName("LastName");

			RuleFor(a => a.About)
				.NotEmpty().WithMessage("About cannot be empty.")
				.Length(3, 150).WithMessage("About must be between 3 and 150 characters.")
				.WithName("About");

			RuleFor(register => register.Number)
				.NotEmpty().WithMessage("Number cannot be empty")
				.Matches(@"^\+?\d{10,15}$").WithMessage("Please enter a valid PhoneNumber.")
				.WithName("Number");

			RuleFor(a => a.Experience)
				.NotEmpty().WithMessage("Experience cannot be empty.")
				.Length(3, 150).WithMessage("Experience must be between 3 and 150 characters.")
				.WithName("Experience");
		}
    }
}
