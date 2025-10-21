using ExamProject.Domain.Entities;

namespace ExamProject.Application.Interfaces.IRepositories {

    public interface IUserExamRepo : IBaseRepo<UserExamEntity> {

        public UserExamEntity? GetUserExam(int userId, int examId);

        public List<UserExamEntity> GetUserExamsForUser(int userId);

        public List<UserExamEntity> GetUserExamsForExam(int examId, int page = 1, int pageSize = 10);

        public List<UserExamEntity> GetUnpassedUserExamsForUser(int userId);

        public List<UserExamEntity> GetCompletedUserExamsForUser(int userId, int page = 1, int pageSize = 10);
        public List<UserExamEntity> GetAllUserExamsForUser( int page = 1, int pageSize = 10);
    }
}