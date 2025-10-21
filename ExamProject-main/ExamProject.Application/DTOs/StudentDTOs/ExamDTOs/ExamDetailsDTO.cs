using ExamProject.Application.DTOs.StudentDTOs.QuestionDTOs;

namespace ExamProject.Application.DTOs.StudentDTOs.ExamDTOs {

    public class ExamDetailsDTO {
        public string ExamName { get; set; }
        public int MaxMarks { get; set; }

        public int TotalScore { get; set; }
        public bool IsPassed { get; set; }

        public List<QuestionDetailsDTO> Questions { get; set; }
    }
}