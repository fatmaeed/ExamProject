using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamProject.Application.Validators.AccountValidators;

namespace ExamProject.Application.DTOs.AccountDTOs
{
    public class ForgotPasswordDto
    {
        [Required,EmailAddress, IsExisitsEmail]
        public string Email { get; set; }
    }
}
