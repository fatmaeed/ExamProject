using System.ComponentModel.DataAnnotations;

namespace ExamProject.Application.DTOs.AdminDTOs.ExamDTOs {

    public class ExamUpdateDTO {

        [MaxLength(50)]
        public string Name { get; set; }

        [Range(1, 999)]
        public short MinDegree { get; set; }

        [Range(1, 1000)]
        public short MaxDegree { get; set; }

        public TimeSpan Duration { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}