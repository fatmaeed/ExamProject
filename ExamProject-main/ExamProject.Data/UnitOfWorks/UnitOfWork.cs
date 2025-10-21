using ExamProject.Application.Interfaces.IRepositories;
using ExamProject.Application.Interfaces.IUnitOfWorks;
using ExamProject.Infrastructure.Data;
using ExamProject.Infrastructure.Repositories;

namespace ExamProject.Infrastructure.UnitOfWorks {

    public class UnitOfWork : IUnitOfWork {
        private readonly ExamDbContext _context;

        private IExamRepo? examRepo;
        private IQuestionRepo? questionRepo;
        private IUserRepo? userRepo;
        private IUserExamRepo? userExamRepo;
        private IUserExamQuestionRepo? userQuestionRepo;

        public UnitOfWork(ExamDbContext context) {
            _context = context;
        }

        public IExamRepo ExamRepo {
            get {
                if (examRepo == null) {
                    examRepo = new ExamRepo(_context);
                }
                return examRepo;
            }
        }

        public IQuestionRepo QuestionRepo {
            get {
                if (questionRepo == null) {
                    questionRepo = new QuestionRepo(_context);
                }
                return questionRepo;
            }
        }

        public IUserRepo UserRepo {
            get {
                if (userRepo == null) {
                    userRepo = new UserRepo(_context);
                }
                return userRepo;
            }
        }

        public IUserExamRepo UserExamRepo {
            get {
                if (userExamRepo == null) {
                    userExamRepo = new UserExamRepo(_context);
                }
                return userExamRepo;
            }
        }

        public IUserExamQuestionRepo UserQuestionRepo {
            get {
                if (userQuestionRepo == null) {
                    userQuestionRepo = new UserExamQuestionRepo(_context);
                }
                return userQuestionRepo;
            }
        }

        public async Task SaveChangesAsync() {
            await _context.SaveChangesAsync();
        }
    }
}