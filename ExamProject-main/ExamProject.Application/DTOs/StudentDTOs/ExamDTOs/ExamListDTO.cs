using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.Application.DTOs.StudentDTOs.ExamDTOs
{
    public class ExamListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short MinDegree { get; set; }
        public short MaxDegree { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsPassed { get; set; }
    }
}
