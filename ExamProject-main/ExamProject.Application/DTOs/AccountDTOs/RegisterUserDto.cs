using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamProject.Application.Validators.AccountValidators;

namespace ExamProject.Application.DTOs.AccountDTOs
{
    public class RegisterUserDto
    {
        [Required , UniqueUserName]

        public string Username { get; set; }
        [Required , EmailAddress , UniqueEmail]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber {  get; set; }
        [Required , DataType(DataType.Password)]
        public string Password { get; set; }
        [Required , DataType(DataType.Password)]
        [Compare(nameof(Password) ,  ErrorMessage = "The password and confirm password Must be matched")]
        public string ConfirmPassword { get; set; }
    }
}
