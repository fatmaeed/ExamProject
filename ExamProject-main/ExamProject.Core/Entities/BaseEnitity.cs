using ExamProject.Application.Interfaces.IEntity;

namespace ExamProject.Domain.Entities {

    public class BaseEnitity : IBaseEntity {
        public int Id { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool isUpdated { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.Now; 

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}