using FinalProject.BLL.Models.DTOs.RegisterDTOs;
using FluentValidation;

namespace FinalProject.BLL.Models.Validations.Register
{
	public class RegisterCreateCompanyDTOValidator : AbstractValidator<CreateCompanyDTO>
	{
		public RegisterCreateCompanyDTOValidator()
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

			RuleFor(x => x.Password)
		        .NotEmpty().WithMessage("Password is required.")
		        .MinimumLength(8).WithMessage("Password must be at least 8 characters long.") 
		        .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.") 
		        .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.") 
		        .Matches("[0-9]").WithMessage("Password must contain at least one number.") 
		        .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

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
