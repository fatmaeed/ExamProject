using ExamProject.Application.Interfaces.IRepositories;

namespace ExamProject.Application.Interfaces.IUnitOfWorks {

    public interface IUnitOfWork {
        public IExamRepo ExamRepo { get; }

        public IQuestionRepo QuestionRepo { get; }

        public IUserRepo UserRepo { get; }

        public IUserExamRepo UserExamRepo { get; }

        public IUserExamQuestionRepo UserQuestionRepo { get; }

        public Task SaveChangesAsync();
    }
}