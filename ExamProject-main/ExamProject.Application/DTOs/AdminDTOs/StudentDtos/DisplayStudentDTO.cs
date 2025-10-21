namespace ExamProject.Application.DTOs.AdminDTOs.StudentDtos {

    public class DisplayStudentDTO {
        public string FullName { get; set; }
        public bool? isPassed { get; set; }

        public int? Marks { get; set; }
    }
}