using FinalProject.BLL.Models.DTOs.VacancyDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.Validations.Vacancy
{
	public class CreateVacancyDTOValidate : AbstractValidator<CreateVacancyDTO>
	{
        public CreateVacancyDTOValidate()
        {
			RuleFor(x => x.HeaderName)
			.NotEmpty().WithMessage("Header name is required.") 
			.MaximumLength(100).WithMessage("Header name must not exceed 100 characters.");

			RuleFor(x => x.Responsibilities)
				.NotEmpty().WithMessage("Responsibilities are required.") 
				.MinimumLength(10).WithMessage("Responsibilities must be at least 10 characters long.");

			RuleFor(x => x.Requirements)
				.NotEmpty().WithMessage("Requirements are required.") 
				.MinimumLength(10).WithMessage("Requirements must be at least 10 characters long."); 

			RuleFor(x => x.CreateDate)
				.NotEmpty().WithMessage("Create date is required.") 
				.LessThanOrEqualTo(DateTime.Now).WithMessage("Create date cannot be in the future."); 

			RuleFor(x => x.ExpireDate)
				.NotEmpty().WithMessage("Expire date is required.") 
				.GreaterThan(x => x.CreateDate).WithMessage("Expire date must be after the create date."); 

			RuleFor(x => x.CategoryName)
				.NotEmpty().WithMessage("Category name is required.") 
				.MaximumLength(50).WithMessage("Category name must not exceed 50 characters."); 
		}
    }
}
