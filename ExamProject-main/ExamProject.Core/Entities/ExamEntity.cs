using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamProject.Domain.Entities {

    public class ExamEntity : BaseEnitity {

        [MaxLength(50)]
        public string Name { get; set; }

        [Range(1, 999), Column(TypeName = "smallint")]
        public short MinDegree { get; set; }

        [Range(1, 1000), Column(TypeName = "smallint")]
        public short MaxDegree { get; set; }

        public TimeSpan Duration { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public virtual ICollection<QuestionEntity> Questions { get; set; }

        public virtual ICollection<UserExamEntity> UserExams { get; set; }
    }
}