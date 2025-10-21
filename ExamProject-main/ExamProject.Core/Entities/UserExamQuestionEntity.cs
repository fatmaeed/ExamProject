using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamProject.Domain.Entities {

    [Table("UserExamQuestion"), PrimaryKey("Id", "QuestionId", "ExamId")]
    public class UserExamQuestionEntity : BaseEnitity {

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        [ForeignKey("Exam")]
        public int ExamId { get; set; }

        [Range(0, 20)]
        public short AnswerScore { get; set; }

        public int SelectedAnswer { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }

        public virtual QuestionEntity Question { get; set; }
        public virtual ExamEntity Exam { get; set; }
    }
}