using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ExamProject.Application.Validators.AccountValidators
{
    internal class IsExisitsEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            UserManager<ApplicationUser> _userManager = validationContext.GetService<UserManager<ApplicationUser>>()!;

            var result = _userManager.FindByEmailAsync(value!.ToString()!).Result;

            if (result != null && result.EmailConfirmed)
                return ValidationResult.Success!;
            return new ValidationResult("Email Not exists");
        }
    }
}
