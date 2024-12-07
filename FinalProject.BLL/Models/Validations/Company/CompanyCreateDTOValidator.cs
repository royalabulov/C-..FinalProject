using FinalProject.BLL.Models.DTOs.CompanyDTOs;
using FinalProject.BLL.Models.DTOs.RegisterDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.Validations.Company
{
	public class CompanyCreateDTOValidator : AbstractValidator<CompanyCreateDTO>
	{
		public CompanyCreateDTOValidator()
		{
			RuleFor(x => x.Name)
		        .NotEmpty().WithMessage("Company name is required.")
		        .MaximumLength(100).WithMessage("Company name must not exceed 100 characters.");

			RuleFor(x => x.About)
				.NotEmpty().WithMessage("About field is required.")
				.MaximumLength(500).WithMessage("About field must not exceed 500 characters.");

			RuleFor(x => x.Address)
				.NotEmpty().WithMessage("Address is required.")
				.MaximumLength(200).WithMessage("Address must not exceed 200 characters.");

			RuleFor(x => x.ContactNumber)
				.NotEmpty().WithMessage("Contact number is required.")
				.Matches(@"^\+?\d{10,15}$").WithMessage("Contact number must be a valid phone number.");
		}
	}
}
