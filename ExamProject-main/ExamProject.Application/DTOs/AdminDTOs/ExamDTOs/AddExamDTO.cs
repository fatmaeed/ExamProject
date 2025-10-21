using ExamProject.Application.DTOs.AdminDTOs.QuestionDTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamProject.Application.DTOs.AdminDTOs.ExamDTOs {

    public class AddExamDTO {
        public string Name { get; set; }

        [Range(1, 999), Column(TypeName = "smallint")]
        public short MinDegree { get; set; }

        [Range(1, 1000), Column(TypeName = "smallint")]
        public short MaxDegree { get; set; }

        public TimeSpan Duration { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public virtual ICollection<AddQuestionDTO> Questions { get; set; }
    }
}