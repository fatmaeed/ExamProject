using AutoMapper;
using ExamProject.Application.DTOs.AdminDTOs.ExamStudentsDTOs;
using ExamProject.Application.DTOs.AdminDTOs.QuestionDTOs;
using ExamProject.Application.DTOs.AdminDTOs.StudentDtos;
using ExamProject.Application.Interfaces.IServices;
using ExamProject.Application.Interfaces.IUnitOfWorks;
using ExamProject.Application.Utils;
using ExamProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Application.Services {

    public class AdminService : IAdminService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminService(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Either<Failure, ExamStudentsDTO>> GetExamStudents(int id) {
            try {
                ExamEntity? exam = await _unitOfWork.ExamRepo.GetByIdAsync(id);
                if (exam == null) return Either<Failure, ExamStudentsDTO>.Failure(new NotFoundFailure("Exam not found"));
                ExamStudentsDTO examStudentsDTO = _mapper.Map<ExamStudentsDTO>(exam);
                examStudentsDTO.Students = _mapper.Map<List<DisplayStudentDTO>>(_unitOfWork.UserExamRepo.GetUserExamsForExam(id));
                return Either<Failure, ExamStudentsDTO>.Success(examStudentsDTO);
            } catch (Exception ex) {
                return Either<Failure, ExamStudentsDTO>.Failure(new Failure(ex.Message));
            }
        }

        public async Task<Either<Failure, UpdateQuestionDTO>> CreateQuestion(int examId, CreateQuestionDTO createQuestionDTO) {
            try {
                if (examId != createQuestionDTO.ExamId) return Either<Failure, UpdateQuestionDTO>.Failure(new NotFoundFailure("Exam not found"));
                QuestionEntity question = await _unitOfWork.QuestionRepo.AddAsync(_mapper.Map<QuestionEntity>(createQuestionDTO));
                await _unitOfWork.SaveChangesAsync();
                return Either<Failure, UpdateQuestionDTO>.Success(_mapper.Map<UpdateQuestionDTO>(question));
            } catch (ArgumentException ex) {
                return Either<Failure, UpdateQuestionDTO>.Failure(new NotFoundFailure("Exam not found"));
            } catch (DbUpdateException ex) {
                return Either<Failure, UpdateQuestionDTO>.Failure(new NotFoundFailure(ex.Message));
            }
        }

        public async Task<Either<Failure, DisplayQuestionDTO>> GetQuestionById(int id) {
            try {
                QuestionEntity? question = await _unitOfWork.QuestionRepo.GetByIdAsync(id);
                if (question == null) return Either<Failure, DisplayQuestionDTO>.Failure(new NotFoundFailure("Question not found"));
                return Either<Failure, DisplayQuestionDTO>.Success(_mapper.Map<DisplayQuestionDTO>(question));
            } catch (Exception ex) {
                return Either<Failure, DisplayQuestionDTO>.Failure(new Failure(ex.Message));
            }
        }

        public async Task<Either<Failure, List<DisplayQuestionDTO>>> GetAllQuestionsForExam(int examId, int page, int pageSize) {
            try {
                ExamEntity? exam = await _unitOfWork.ExamRepo.GetByIdAsync(examId);
                if (exam == null) return Either<Failure, List<DisplayQuestionDTO>>.Failure(new NotFoundFailure("Exam not found"));
                List<QuestionEntity> questions = _unitOfWork.QuestionRepo.GetQuestionsByExamId(examId, page, pageSize);
                return Either<Failure, List<DisplayQuestionDTO>>.Success(_mapper.Map<List<DisplayQuestionDTO>>(questions));
            } catch (Exception ex) {
                return Either<Failure, List<DisplayQuestionDTO>>.Failure(new Failure(ex.Message));
            }
        }

        public async Task<Either<Failure, BaseQuestionDTO>> DeleteQuestion(int id) {
            try {
                QuestionEntity? question = await _unitOfWork.QuestionRepo.GetByIdAsync(id);
                if (question == null) return Either<Failure, BaseQuestionDTO>.Failure(new NotFoundFailure("Question not found"));
                await _unitOfWork.QuestionRepo.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                return Either<Failure, BaseQuestionDTO>.Success(_mapper.Map<BaseQuestionDTO>(question));
            } catch (Exception ex) {
                return Either<Failure, BaseQuestionDTO>.Failure(new Failure(ex.Message));
            }
        }

        public async Task<Either<Failure, UpdateQuestionDTO>> UpdateQuestion(int id, UpdateQuestionDTO updateQuestionDTO) {
            try {
                QuestionEntity? question = await _unitOfWork.QuestionRepo.GetByIdAsync(id);
                if (question == null) return Either<Failure, UpdateQuestionDTO>.Failure(new NotFoundFailure("Question not found"));
                if (question.ExamId != updateQuestionDTO.ExamId) return Either<Failure, UpdateQuestionDTO>.Failure(new NotFoundFailure("Question does not belong to this Exam"));
                if (id != updateQuestionDTO.Id) return Either<Failure, UpdateQuestionDTO>.Failure(new NotFoundFailure("Question not found"));
                await _unitOfWork.QuestionRepo.Update(_mapper.Map(updateQuestionDTO, question));
                await _unitOfWork.SaveChangesAsync();
                return Either<Failure, UpdateQuestionDTO>.Success(updateQuestionDTO);
            } catch (DbUpdateException ex) {
                return Either<Failure, UpdateQuestionDTO>.Failure(new NotFoundFailure(ex.Message));
            } catch (Exception ex) {
                return Either<Failure, UpdateQuestionDTO>.Failure(new Failure(ex.Message));
            }
        }

        public Either<Failure, List<DisplayQuestionDTO>> SearchQuestion(int id, string search, int page, int pageSize) {
            try {
                List<QuestionEntity> questions = _unitOfWork.QuestionRepo.SearchAboutQuestions(id, search, page, pageSize);
                return Either<Failure, List<DisplayQuestionDTO>>.Success(_mapper.Map<List<DisplayQuestionDTO>>(questions));
            } catch (Exception ex) {
                return Either<Failure, List<DisplayQuestionDTO>>.Failure(new Failure(ex.Message));
            }
        }
    }
}