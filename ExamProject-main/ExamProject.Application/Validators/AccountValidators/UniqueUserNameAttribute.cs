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
    public class UniqueUserNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            UserManager<ApplicationUser> _userManager = validationContext.GetService<UserManager<ApplicationUser>>();

            var result = _userManager.FindByNameAsync(value!.ToString()).Result;

            if (result == null)
                return ValidationResult.Success;
            return new ValidationResult(" UserName already exists");
        }
    }
}
