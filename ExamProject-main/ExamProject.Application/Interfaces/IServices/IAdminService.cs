using ExamProject.Application.DTOs.AdminDTOs.ExamStudentsDTOs;
using ExamProject.Application.DTOs.AdminDTOs.QuestionDTOs;
using ExamProject.Application.Utils;

namespace ExamProject.Application.Interfaces.IServices {

    public interface IAdminService {

        public Task<Either<Failure, ExamStudentsDTO>> GetExamStudents(int id);

        public Task<Either<Failure, UpdateQuestionDTO>> CreateQuestion(int examId, CreateQuestionDTO createQuestionDTO);

        public Task<Either<Failure, DisplayQuestionDTO>> GetQuestionById(int id);

        public Task<Either<Failure, List<DisplayQuestionDTO>>> GetAllQuestionsForExam(int examId, int page, int pageSize);

        public Task<Either<Failure, BaseQuestionDTO>> DeleteQuestion(int id);

        public Task<Either<Failure, UpdateQuestionDTO>> UpdateQuestion(int id, UpdateQuestionDTO updateQuestionDTO);

        public Either<Failure, List<DisplayQuestionDTO>> SearchQuestion(int id, string search, int page, int pageSize);
    }
}