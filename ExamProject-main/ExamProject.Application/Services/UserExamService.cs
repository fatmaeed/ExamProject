using AutoMapper;
using ExamProject.Application.DTOs.StudentDTOs.UserExamDTOs;
using ExamProject.Application.Interfaces.IServices;
using ExamProject.Application.Interfaces.IUnitOfWorks;
using ExamProject.Domain.Entities;

namespace ExamProject.Application.Services {

    public class UserExamService : IUserExamService {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserExamService(IUnitOfWork unitOfWork, IMapper mapper) {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public List<CompletedUserExamsDTO> GetPassedExamsAsync(int userId) {
            List<UserExamEntity> userExams = unitOfWork.UserExamRepo.GetCompletedUserExamsForUser(userId);
            return mapper.Map<List<CompletedUserExamsDTO>>(userExams);
        }

        public List<CompletedUserExamsDTO> GetUnpassedUserExamsForUser(int userId) {
            List<UserExamEntity> userExams = unitOfWork.UserExamRepo.GetUnpassedUserExamsForUser(userId);
            return mapper.Map<List<CompletedUserExamsDTO>>(userExams);
        }

        public List<CompletedUserExamsDTO> GetAllExamsForUser()
        {
            List<UserExamEntity> userExams = unitOfWork.UserExamRepo.GetAllUserExamsForUser();
            return mapper.Map<List<CompletedUserExamsDTO>>(userExams);
        }

    }
}