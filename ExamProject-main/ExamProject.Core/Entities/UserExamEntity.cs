using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamProject.Domain.Entities {

    [Table("UserExams"), PrimaryKey(nameof(Id), nameof(ExamId))]
    public class UserExamEntity : BaseEnitity {

        [ForeignKey("Exam")]
        public int ExamId { get; set; }

        public int TotalScore { get; set; } = 0;
        public bool IsCompleted { get; set; } = false;

        public bool? IsPassed { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }

        public virtual ExamEntity Exam { get; set; }
    }
}