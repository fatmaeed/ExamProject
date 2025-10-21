namespace ExamProject.Application.DTOs.StudentDTOs.QuestionDTOs {

    public class QuestionResultDTO {
        public int QuestionId { get; set; }
        public int SelectedAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public int Score { get; set; }
        public string CorrectAnswer { get; set; }
    }
}