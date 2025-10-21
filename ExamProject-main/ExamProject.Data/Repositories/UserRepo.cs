using ExamProject.Application.Interfaces.IRepositories;
using ExamProject.Domain.Entities;
using ExamProject.Infrastructure.Data;

namespace ExamProject.Infrastructure.Repositories {

    public class UserRepo : BaseRepo<ApplicationUser>, IUserRepo {

        public UserRepo(ExamDbContext examDb) : base(examDb) {
        }
    }
}