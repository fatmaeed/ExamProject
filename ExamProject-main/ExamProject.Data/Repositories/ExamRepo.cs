using ExamProject.Application.Interfaces.IRepositories;
using ExamProject.Domain.Entities;
using ExamProject.Infrastructure.Data;
using ExamProject.Infrastructure.Repositories;

namespace ExamProject.Infrastructure.Repositories {

    public class ExamRepo : BaseRepo<ExamEntity>, IExamRepo {

        public ExamRepo(ExamDbContext examDb) : base(examDb) {
        }
        public List<int> GetIdsForAllExams()
        {
            return examDb.Exams.Select(e =>  e.Id).ToList();
        }
    }
}
