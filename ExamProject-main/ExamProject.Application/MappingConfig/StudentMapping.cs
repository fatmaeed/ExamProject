using AutoMapper;
using ExamProject.Application.DTOs.StudentDTOs.ExamDTOs;
using ExamProject.Application.DTOs.StudentDTOs.QuestionDTOs;
using ExamProject.Application.DTOs.StudentDTOs.UserExamDTOs;
using ExamProject.Domain.Entities;

namespace ExamProject.Application.MappingConfig {

    public class StudentMapping : Profile {

        public StudentMapping() {
            CreateMap<QuestionEntity, StudentQuestionDTO>()
             .AfterMap((src, dist) => {
                 dist.QuestionText = src.Text;
                 dist.QuestionId = src.Id;
             }).ReverseMap();

            CreateMap<UserExamEntity, CompletedUserExamsDTO>().AfterMap((src, dest) => {
                dest.ExamName = src.Exam.Name;
                dest.MaxScore = src.Exam.MaxDegree;
                dest.Duration = src.Exam.Duration;
            });

            CreateMap<UserExamEntity, UnCompletedUserExamsDTO>().
                AfterMap((src, dest) => {
                    dest.ExamName = src.Exam.Name;
                    dest.MaxScore = src.Exam.MaxDegree;
                    dest.Exam.NumberOfQuestions = src.Exam.Questions?.Where(q => !q.IsDeleted).Count() ?? 0;
                    dest.TotalScore = src.Exam.Questions?.Where(q => !q.IsDeleted)?.Sum(q => q.Score) ?? 0;
                });

            CreateMap<UserExamEntity, ExamDetailsDTO>().AfterMap((src, dest) => {
                dest.ExamName = src.Exam.Name;
                dest.MaxMarks = src.Exam.MaxDegree;
                dest.IsPassed = src.IsPassed ?? false;
            });
            CreateMap<UserExamQuestionEntity, QuestionDetailsDTO>().AfterMap((src, dest) => {
                dest.QuestionText = src.Question.Text;
                dest.CorrectAnswer = src.Question.CorrectAnswer;
                dest.Score = src.Question.Score;
                dest.AnswerScore = src.AnswerScore;
                dest.Id = src.QuestionId;
                dest.Choice1 = src.Question.Choice1;
                dest.Choice2 = src.Question.Choice2;
                dest.Choice3 = src.Question.Choice3;
                dest.Choice4 = src.Question.Choice4;
            });
            CreateMap<ExamListDTO, ExamEntity>().ReverseMap();
        }
    }
}