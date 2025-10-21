namespace ExamProject.Application.DTOs.AdminDTOs.QuestionDTOs {

    public class UpdateQuestionDTO : BaseQuestionDTO {
        public int Id { get; set; }
        public int ExamId { get; set; }
    }
}