using ExamProject.Application.Interfaces.IRepositories;
using ExamProject.Domain.Entities;
using ExamProject.Infrastructure.Data;

namespace ExamProject.Infrastructure.Repositories {

    public class QuestionRepo : BaseRepo<QuestionEntity>, IQuestionRepo {

        public QuestionRepo(ExamDbContext examDb) : base(examDb) {
        }

        public List<QuestionEntity> GetQuestionsByExamId(int id, int page, int pageSize) {
            return examDb.Questions.Where(x => x.ExamId == id && !x.IsDeleted).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<QuestionEntity> SearchAboutQuestions(int id, string search, int page, int pageSize) {
            if (string.IsNullOrEmpty(search)) return GetQuestionsByExamId(id, page, pageSize);
            return examDb.Questions.Where(x => x.ExamId == id && x.Text.Contains(search) && !x.IsDeleted).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}