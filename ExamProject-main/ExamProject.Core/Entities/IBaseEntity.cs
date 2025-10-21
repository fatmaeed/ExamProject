namespace ExamProject.Application.Interfaces.IEntity {

    public interface IBaseEntity {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        public bool isUpdated { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}