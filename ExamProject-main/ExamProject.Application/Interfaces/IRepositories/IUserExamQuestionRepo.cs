using ExamProject.Domain.Entities;

namespace ExamProject.Application.Interfaces.IRepositories {

    public interface IUserExamQuestionRepo : IBaseRepo<UserExamQuestionEntity> {
        Task<UserExamQuestionEntity?> GetByUserExamQuestionAsync(int userId, int examId, int questionId);
        List<UserExamQuestionEntity> GetQuestionByUserExam(int userId, int examId);
    }
}