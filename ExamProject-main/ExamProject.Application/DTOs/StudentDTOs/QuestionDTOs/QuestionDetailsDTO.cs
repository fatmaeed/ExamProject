using ExamProject.Application.DTOs.AdminDTOs.QuestionDTOs;

namespace ExamProject.Application.DTOs.StudentDTOs.QuestionDTOs {
    public class QuestionDetailsDTO : DisplayQuestionDTO {

        public int AnswerScore { get; set; }

        public bool isCorrect { get { return AnswerScore > 0; } }

    }
}
