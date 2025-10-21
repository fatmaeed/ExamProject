using ExamProject.Domain.Entities;

namespace ExamProject.Application.Interfaces.IRepositories {

    public interface IQuestionRepo : IBaseRepo<QuestionEntity> {

        public List<QuestionEntity> GetQuestionsByExamId(int id, int page, int pageSize);

        public List<QuestionEntity> SearchAboutQuestions(int id, string search, int page, int pageSize);
    }
}