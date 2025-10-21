using ExamProject.Application.Interfaces.IRepositories;
using ExamProject.Domain.Entities;
using ExamProject.Infrastructure.Data;

namespace ExamProject.Infrastructure.Repositories {

    public class UserExamRepo : BaseRepo<UserExamEntity>, IUserExamRepo {

        public UserExamRepo(ExamDbContext examDb) : base(examDb) {
        }

        public List<UserExamEntity> GetCompletedUserExamsForUser(int userId, int page = 1, int pageSize = 10) {
            return examDb.UserExams.Where(x => x.Id == userId && x.IsCompleted == true).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public UserExamEntity? GetUserExam(int userId, int examId) {
            return examDb.UserExams.Where(x => x.Id == userId && x.ExamId == examId).FirstOrDefault();
        }

        public List<UserExamEntity> GetUserExamsForExam(int examId, int page = 1, int pageSize = 10) {
            return examDb.UserExams.Where(x => x.ExamId == examId).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<UserExamEntity> GetUserExamsForUser(int userId) {
            return examDb.UserExams.Where(x => x.Id == userId && !x.IsDeleted).ToList();
        }

        public List<UserExamEntity> GetUnpassedUserExamsForUser(int userId) {
            return examDb.UserExams.Where(x => x.Id == userId && x.IsCompleted != true && !x.IsDeleted).ToList();
        }

        public List<UserExamEntity> GetAllUserExamsForUser(int page = 1, int pageSize = 10)
        {
            return examDb.UserExams.Where(x =>  !x.IsDeleted).AsEnumerable().DistinctBy(x => x.ExamId).ToList();

        }
    }
}