using ExamProject.Application.Interfaces.IEntity;
using Microsoft.AspNetCore.Identity;

namespace ExamProject.Domain.Entities {

    public class ApplicationUser : IdentityUser<int>, IBaseEntity {
        public string?  Name { get; set; }

        public virtual ICollection<UserExamEntity> UserExams { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool isUpdated { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}