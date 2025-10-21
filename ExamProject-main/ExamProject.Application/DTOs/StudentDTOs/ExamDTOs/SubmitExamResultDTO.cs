using ExamProject.Application.DTOs.StudentDTOs.QuestionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.Application.DTOs.StudentDTOs.ExamDTOs
{
    public class SubmitExamResultDTO
    {
        public int ExamId { get; set; }
        public int UserId { get; set; }
        public int TotalScore { get; set; }
        public List<QuestionResultDTO> Details { get; set; }
    }
}
