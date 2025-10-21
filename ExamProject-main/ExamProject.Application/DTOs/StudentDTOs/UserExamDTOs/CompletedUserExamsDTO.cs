namespace ExamProject.Application.DTOs.StudentDTOs.UserExamDTOs {

    public class CompletedUserExamsDTO {
        public string ExamName { get; set; }

        public int MaxScore { get; set; }

        public int TotalScore { get; set; }
        public bool IsCompleted { get; set; }

        public bool? IsPassed { get; set; }
        public int ExamId { get; set; }
        public TimeSpan Duration { get; set; }

        

    }
}