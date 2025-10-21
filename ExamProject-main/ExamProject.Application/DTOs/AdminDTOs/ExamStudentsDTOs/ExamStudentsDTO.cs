using ExamProject.Application.DTOs.AdminDTOs.StudentDtos;

namespace ExamProject.Application.DTOs.AdminDTOs.ExamStudentsDTOs {

    public class ExamStudentsDTO {

        public ExamStudentsDTO() {
            Students = new List<DisplayStudentDTO>();
        }

        public string ExamName { get; set; }
        public short MaxMarks { get; set; }

        public short MinMarks { get; set; }
        public int TotalStudents { get; set; }
        public int PassedStudents { get; set; }
        public int FailedStudents { get; set; }
        public double? AverageMarks { get; set; }
        public TimeSpan Duration { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public List<DisplayStudentDTO> students;

        public List<DisplayStudentDTO> Students {
            get { return students; }
            set {
                students = value;
                AverageMarks = students.Average(s => s.Marks);
                TotalStudents = students.Count;
                PassedStudents = students.Count(s => s.Marks >= MinMarks);
                FailedStudents = students.Count(s => s.Marks < MinMarks);
            }
        }
    }
}