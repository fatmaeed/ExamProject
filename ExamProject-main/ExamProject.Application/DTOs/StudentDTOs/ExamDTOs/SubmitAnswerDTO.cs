namespace ExamProject.Application.DTOs.StudentDTOs.ExamDTOs {

    public class SubmitAnswerDTO {
        public int ExamId { get; set; }
        public int UserId { get; set; }
        public List<AnswerItem> Answers { get; set; }

        public class AnswerItem {
            public int QuestionId { get; set; }
            public int SelectedAnswer { get; set; }
        }
    }
}