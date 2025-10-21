using System.ComponentModel.DataAnnotations;

namespace ExamProject.Application.DTOs.StudentDTOs.QuestionDTOs {

    public class StudentQuestionDTO
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string Choice1 { get; set; }
        public string Choice2 { get; set; }
        public string Choice3 { get; set; }
        public string Choice4 { get; set; }
        public short Score { get; set; }
     

    }

}