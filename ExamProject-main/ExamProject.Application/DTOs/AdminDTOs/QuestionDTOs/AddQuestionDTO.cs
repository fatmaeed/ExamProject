using System.ComponentModel.DataAnnotations;

namespace ExamProject.Application.DTOs.AdminDTOs.QuestionDTOs {

    public class AddQuestionDTO {
        [MaxLength(100)]
        public string QuestionText { get; set; }
        [MaxLength(100)]

        public string Choice1 { get; set; }
        [MaxLength(100)]

        public string Choice2 { get; set; }
        [MaxLength(100)]

        public string Choice3 { get; set; }
        [MaxLength(100)]

        public string Choice4 { get; set; }
        [Range(1, 4)]
        public int CorrectAnswer { get; set; }
        [Range(0, 20)]
        public int Score { get; set; }
    }
}