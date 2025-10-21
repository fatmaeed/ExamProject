namespace ExamProject.Application.DTOs.AdminDTOs.ExamDTOs {

    public class ExamDTO {
        public int Id { get; set; }
        public string Name { get; set; }

        public short MinDegree { get; set; }

        public short MaxDegree { get; set; }

        public TimeSpan Duration { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public int NumberOfQuestions { get; set; }

        public int TotalScore { get; set; }
    }
}