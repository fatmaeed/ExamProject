using ExamProject.Application.DTOs.AdminDTOs.QuestionDTOs;

namespace ExamProject.Application.DTOs.AdminDTOs.ExamDTOs {

    public class GetExamDTO {
        public string Name { get; set; }

        public short MinDegree { get; set; }

        public short MaxDegree { get; set; }

        public TimeSpan Duration { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public int NumberOfQuestions { get; set; }
        public virtual ICollection<DisplayQuestionDTO> Questions { get; set; }
    }
}