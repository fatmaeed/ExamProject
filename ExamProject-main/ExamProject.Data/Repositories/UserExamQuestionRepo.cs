using ExamProject.Application.Interfaces.IRepositories;
using ExamProject.Domain.Entities;
using ExamProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Infrastructure.Repositories {

    public class UserExamQuestionRepo : BaseRepo<UserExamQuestionEntity>, IUserExamQuestionRepo {

        public UserExamQuestionRepo(ExamDbContext examDb) : base(examDb) {
        }

        public List<UserExamQuestionEntity> GetQuestionByUserExam(int userId, int examId) {
            return examDb.UserExamQuestions.Where(uq => uq.Id == userId && uq.ExamId == examId).ToList();
        }

        public async Task<UserExamQuestionEntity?> GetByUserExamQuestionAsync(int userId, int examId, int questionId) {
            return await examDb.UserExamQuestions
                .FirstOrDefaultAsync(uq => uq.Id == userId && uq.ExamId == examId && uq.QuestionId == questionId);
        }
    }
}