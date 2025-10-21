using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.Application.DTOs.AccountDTOs
{
    public class ChangePasswordDto
    {
        [Required]
        [DataType(DataType.Password)]

        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]

        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "The password and confirm password Must be matched")]
        public string ConfirmNewPassword { get; set; }
    }
}
