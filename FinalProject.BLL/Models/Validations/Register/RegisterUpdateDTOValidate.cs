using FinalProject.BLL.Models.DTOs.RegisterDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.Validations.Register
{
	public class RegisterUpdateDTOValidate: AbstractValidator<UserUpdateDTO>
	{
        public RegisterUpdateDTOValidate()
        {
			RuleFor(a => a.FirsName)
			   .NotEmpty().WithMessage("Username cannot be empty.")
			   .Length(3, 40).WithMessage("Username must be between 3 and 40 characters.")
			   .WithName("FirstName");

			RuleFor(a => a.LastName)
				.NotEmpty().WithMessage("LastName cannot be empty.")
				.Length(3, 40).WithMessage("LastName must be between 3 and 40 characters.")
				.WithName("LastName");

			RuleFor(a => a.Email)
				.NotEmpty().WithMessage("Email cannot be empty.")
				.EmailAddress().WithMessage("Please enter a valid Email.")
				.WithName("Email");

			RuleFor(a => a.Password)
				.NotEmpty().WithMessage("Password cannot be empty.")
				.MinimumLength(6).WithMessage("Password must be at least 6 characters.")
				.WithName("Password");

			RuleFor(a => a.ConfirmPassword)
				.NotEmpty().WithMessage("Password cannot be empty.")
				.Equal(a => a.Password).WithMessage("Passwords do not match.")
				.WithName("ConfirmPassword");

			RuleFor(register => register.Address)
				.NotEmpty().WithMessage("Address cannot be empty.")
				.MaximumLength(200).WithMessage("Address should not exceed 200 characters.")
				.WithName("Address");

			RuleFor(register => register.DateOfBirth)
				.NotEmpty().WithMessage("DateOfBirth cannot be empty.")
				.LessThan(DateTime.Now).WithMessage("DateOfBitrh cannot be in the future.")
				.WithName("DateOfBitrh");

			RuleFor(register => register.PhoneNumber)
				.NotEmpty().WithMessage("PhoneNumber cannot be empty")
				.Matches(@"^\+?\d{10,15}$").WithMessage("Please enter a valid PhoneNumber.")
				.WithName("PhoneNumber");

		}
	}
}
