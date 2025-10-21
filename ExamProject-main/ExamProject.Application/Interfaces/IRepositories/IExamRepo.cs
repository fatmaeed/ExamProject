using ExamProject.Domain.Entities;

namespace ExamProject.Application.Interfaces.IRepositories {

    public interface IExamRepo : IBaseRepo<ExamEntity> {

        public List<int> GetIdsForAllExams();



    }
}