using ExamProject.Application.DTOs.AdminDTOs.ExamDTOs;

namespace ExamProject.Application.DTOs.StudentDTOs.UserExamDTOs {

    public class UnCompletedUserExamsDTO {
        public string ExamName { get; set; }

        public int MaxScore { get; set; }

        public int TotalScore { get; set; }
        public bool IsCompleted { get; set; }

        public bool? IsPassed { get; set; }

        public ExamDTO Exam { get; set; }
    }
}