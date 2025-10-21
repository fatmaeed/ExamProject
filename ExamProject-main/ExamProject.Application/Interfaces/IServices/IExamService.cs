using ExamProject.Application.DTOs.AdminDTOs.ExamDTOs;
using ExamProject.Application.DTOs.StudentDTOs.ExamDTOs;
using ExamProject.Application.DTOs.StudentDTOs.QuestionDTOs;

namespace ExamProject.Application.Interfaces.IServices {

    public interface IExamService {

        Task<List<ExamDTO>> GetAllExamAsync();

        Task<ExamDTO> GetExamByIdAsync(int id);

        Task AddAsync(AddExamDTO examDTO);

        Task<ExamDTO> Delete(int id);

        Task Update(int examId, ExamUpdateDTO examUpdateDTO);

        Task<GetExamDTO> GetExamWithQuestionsAsync(int id);

        //Task<List<ExamListDTO>> GetAllUncompletedExamsAsync(int userId);

        Task<List<StudentQuestionDTO>> GetExamQuestionsAsync(int examId);

        Task<SubmitExamResultDTO> SubmitExamAsync(SubmitAnswerDTO model);

        Task<List<SearchDTO>> SearchAsync(string name);

        ExamDetailsDTO GetExamDetails(int userId, int examId);
    }
}