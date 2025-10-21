using ExamProject.Application.DTOs.StudentDTOs.UserExamDTOs;

namespace ExamProject.Application.Interfaces.IServices {

    public interface IUserExamService {

        public List<CompletedUserExamsDTO> GetPassedExamsAsync(int userId);

        public List<CompletedUserExamsDTO> GetUnpassedUserExamsForUser(int userId);
        public List<CompletedUserExamsDTO> GetAllExamsForUser();


    }
}