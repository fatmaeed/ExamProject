using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamProject.Domain.Entities {

    public class QuestionEntity : BaseEnitity {

        [MaxLength(150)]
        public string Text { get; set; }

        [MaxLength(150)]
        public string Choice1 { get; set; }

        [MaxLength(150)]
        public string Choice2 { get; set; }

        [MaxLength(150)]
        public string Choice3 { get; set; }

        [MaxLength(150)]
        public string Choice4 { get; set; }

        [Range(1, 4)]
        public int CorrectAnswer { get; set; }

        [Range(0, 20)]
        public short Score { get; set; }

        [ForeignKey("Exam")]
        public int ExamId { get; set; }

        public virtual ExamEntity Exam { get; set; }
    }
}