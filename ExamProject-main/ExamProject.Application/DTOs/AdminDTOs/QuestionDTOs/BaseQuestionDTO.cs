using System.ComponentModel.DataAnnotations;

namespace ExamProject.Application.DTOs.AdminDTOs.QuestionDTOs {

    public class BaseQuestionDTO {
        public string QuestionText { get; set; }
        public string Choice1 { get; set; }
        public string Choice2 { get; set; }
        public string Choice3 { get; set; }
        public string Choice4 { get; set; }
        public short Score { get; set; }

        [Range(1, 4)]
        public int CorrectAnswer { get; set; }
    }
}