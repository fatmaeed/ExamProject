using AutoMapper;
using ExamProject.Application.DTOs.AdminDTOs.ExamDTOs;
using ExamProject.Application.DTOs.StudentDTOs.ExamDTOs;
using ExamProject.Application.DTOs.StudentDTOs.QuestionDTOs;
using ExamProject.Application.Interfaces.IServices;
using ExamProject.Application.Interfaces.IUnitOfWorks;
using ExamProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Application.Services
{

    public class ExamService : IExamService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ExamService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddAsync(AddExamDTO examDTO)
        {
            var exam = mapper.Map<ExamEntity>(examDTO);
            await unitOfWork.ExamRepo.AddAsync(exam);
            await unitOfWork.SaveChangesAsync();
            if (examDTO.Questions != null)
            {
                foreach (var question in examDTO.Questions)
                {
                    QuestionEntity q = new QuestionEntity
                    {
                        Text = question.QuestionText,
                        Choice1 = question.Choice1,
                        Choice2 = question.Choice2,
                        Choice3 = question.Choice3,
                        Choice4 = question.Choice4,
                        CorrectAnswer = question.CorrectAnswer,
                        Score = (short)question.Score,
                        ExamId = exam.Id,
                    };
                    await unitOfWork.QuestionRepo.AddAsync(q);
                }
                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<ExamDTO?> Delete(int id)
        {
            var exam = await unitOfWork.ExamRepo.Delete(id);

            if (exam == null)
            {
                return null;
            }
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<ExamDTO>(exam);
        }

        public async Task<List<ExamDTO>> GetAllExamAsync()
        {
            var exams = unitOfWork.ExamRepo.GetAllAsync();
            var examList = await exams.ToListAsync();
            return mapper.Map<List<ExamDTO>>(examList);
        }

        public async Task<ExamDTO> GetExamByIdAsync(int id)
        {
            var exam = await unitOfWork.ExamRepo.GetByIdAsync(id);
            return mapper.Map<ExamDTO>(exam);
        }

        public async Task Update(int examId, ExamUpdateDTO examUpdateDTO)
        {
            var examfromDb = await unitOfWork.ExamRepo.GetByIdAsync(examId);

            mapper.Map(examUpdateDTO, examfromDb);
            examfromDb.UpdatedDate = DateTime.Now;
            examfromDb.UpdatedDate = DateTime.Now;
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<GetExamDTO?> GetExamWithQuestionsAsync(int id)
        {
            var exam = await unitOfWork.ExamRepo.GetByIdAsync(id);
            if (exam == null)
            {
                return null;
            }
            exam.Questions = exam.Questions.Where(q => !q.IsDeleted).ToList();
            return mapper.Map<GetExamDTO>(exam);
        }

        public async Task<List<SearchDTO>> SearchAsync(string name) {
            var exams = unitOfWork.ExamRepo.GetAllAsync();
            var searchedResult = await exams.Where(e => e.Name.Contains(name)).ToListAsync();
            return mapper.Map<List<SearchDTO>>(searchedResult);
        }

        //public async Task<List<ExamListDTO>> GetAllUncompletedExamsAsync(int userId) {
        //    var allExams = unitOfWork.ExamRepo.GetAllAsync();
        //    var userExams = unitOfWork.UserExamRepo.GetUserExamsForUser(userId);
        //    var result = new List<ExamListDTO>();

        // foreach (var exam in allExams) { var userExam = userExams.FirstOrDefault(userEx =>
        // userEx.ExamId == exam.Id); bool isPassed = (userExam?.TotalScore ?? 0) >= exam.MinDegree;

        //        if (!isPassed) {
        //            var dto = mapper.Map<ExamListDTO>(exam);
        //            dto.IsPassed = isPassed;
        //            result.Add(dto);
        //        }
        //    }
        //    return result;
        //}

        public async Task<List<StudentQuestionDTO>> GetExamQuestionsAsync(int examId)
        {
            var questions = unitOfWork.QuestionRepo.GetAllAsync().Where(q => q.ExamId == examId);
            return mapper.Map<List<StudentQuestionDTO>>(questions.ToList());
        }

        public async Task<SubmitExamResultDTO> SubmitExamAsync(SubmitAnswerDTO model)
        {
            int totalScore = 0;
            var result = new List<QuestionResultDTO>();

            foreach (var answer in model.Answers)
            {
                var question = await unitOfWork.QuestionRepo.GetByIdAsync(answer.QuestionId);
                if (question == null || question.ExamId != model.ExamId) continue;

                bool isCorrect = question.CorrectAnswer == answer.SelectedAnswer;
                int score = isCorrect ? question.Score : 0;
                totalScore += score;
                var existing = await unitOfWork.UserQuestionRepo
                    .GetByUserExamQuestionAsync(model.UserId, model.ExamId, question.Id);

                if (existing == null)
                {
                    await unitOfWork.UserQuestionRepo.AddAsync(new UserExamQuestionEntity
                    {
                        Id = model.UserId,
                        ExamId = model.ExamId,
                        QuestionId = question.Id,
                        SelectedAnswer = answer.SelectedAnswer,
                        AnswerScore = (short)score
                    });
                }
                else
                {
                    existing.SelectedAnswer = answer.SelectedAnswer;
                    existing.AnswerScore = (short)score;
                }

                result.Add(new QuestionResultDTO
                {
                    QuestionId = question.Id,
                    SelectedAnswer = answer.SelectedAnswer,
                    IsCorrect = isCorrect,
                    Score = score,
                    CorrectAnswer = MapCorrectAnswer(question)

                });
            }
            var userExam = unitOfWork.UserExamRepo.GetUserExam(model.UserId, model.ExamId);
            if (userExam != null) {
                userExam.TotalScore = (short)totalScore;
                userExam.IsCompleted = true;
                userExam.IsPassed = userExam.TotalScore >= userExam.Exam.MinDegree;
            }
            //} else {
            //    await unitOfWork.UserExamRepo.AddAsync(new UserExamEntity {
            //        Id = model.UserId,
            //        ExamId = model.ExamId,
            //        IsCompleted = true,
            //        TotalScore = (short)totalScore

            //    });
            //}

            await unitOfWork.SaveChangesAsync();

            return new SubmitExamResultDTO
            {
                ExamId = model.ExamId,
                UserId = model.UserId,
                TotalScore = totalScore,
                Details = result

            };
        }

        public ExamDetailsDTO GetExamDetails(int userId, int examId) {
            List<UserExamQuestionEntity> quesions = unitOfWork.UserQuestionRepo.GetQuestionByUserExam(userId, examId);
            UserExamEntity userExam = unitOfWork.UserExamRepo.GetUserExam(userId, examId);
            ExamDetailsDTO dto = mapper.Map<ExamDetailsDTO>(userExam);
            dto.Questions = mapper.Map<List<QuestionDetailsDTO>>(quesions);
            return dto;
        }

        public string MapCorrectAnswer(QuestionEntity ques)
        {
            string result = string.Empty;
            switch (ques.CorrectAnswer)
            {
                case 1: result = ques.Choice1; break;
                case 2: result = ques.Choice2; break;
                case 3: result = ques.Choice3; break;
                case 4: result = ques.Choice4; break;
            }
            return result;
        }
    }
}