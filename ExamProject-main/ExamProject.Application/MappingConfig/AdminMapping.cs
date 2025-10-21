using AutoMapper;
using ExamProject.Application.DTOs.AdminDTOs.ExamDTOs;
using ExamProject.Application.DTOs.AdminDTOs.ExamStudentsDTOs;
using ExamProject.Application.DTOs.AdminDTOs.QuestionDTOs;
using ExamProject.Application.DTOs.AdminDTOs.StudentDtos;
using ExamProject.Domain.Entities;

namespace ExamProject.Application.MappingConfig {

    public class AdminMapping : Profile {

        public AdminMapping() {
            CreateMap<ExamEntity, ExamStudentsDTO>().AfterMap((src, dest) => {
                dest.ExamName = src.Name;
                dest.MaxMarks = src.MaxDegree;
                dest.MinMarks = src.MinDegree;
                dest.Duration = src.Duration;
                dest.StartDateTime = src.StartTime;
                dest.EndDateTime = src.EndTime;
            });

            CreateMap<UserExamEntity, DisplayStudentDTO>().AfterMap((src, dest) => {
                dest.Marks = src.TotalScore;
                dest.isPassed = src.IsPassed;
                dest.FullName = src.User.UserName;
            });

            CreateMap<BaseQuestionDTO, QuestionEntity>().AfterMap((src, dest) => {
                dest.Text = src.QuestionText;
            });

            CreateMap<UpdateQuestionDTO, QuestionEntity>().AfterMap((src, dest) => {
                dest.Id = src.Id;
            }).IncludeBase<BaseQuestionDTO, QuestionEntity>();

            CreateMap<QuestionEntity, BaseQuestionDTO>().AfterMap((src, dest) => {
                dest.QuestionText = src.Text;
            });

            CreateMap<QuestionEntity, DisplayQuestionDTO>().IncludeBase<QuestionEntity, BaseQuestionDTO>();

            CreateMap<QuestionEntity, UpdateQuestionDTO>().AfterMap((src, dest) => {
                dest.Id = src.Id;
            }).IncludeBase<QuestionEntity, BaseQuestionDTO>();

            CreateMap<CreateQuestionDTO, QuestionEntity>().AfterMap((src, dest) => {
                dest.ExamId = src.ExamId;
            }).IncludeBase<BaseQuestionDTO, QuestionEntity>();

            CreateMap<AddExamDTO, ExamEntity>()
            .ForMember(dest => dest.Questions, opt => opt.Ignore());
            CreateMap<ExamEntity, GetExamDTO>().AfterMap((s, d) => {
                d.NumberOfQuestions = s.Questions?.Count() ?? 0;
            });

            CreateMap<ExamEntity, ExamDTO>().AfterMap((s, d) => {
                d.NumberOfQuestions = s.Questions?.Where(q => !q.IsDeleted).Count() ?? 0;
                d.TotalScore = s.Questions?.Where(q => !q.IsDeleted)?.Sum(q => q.Score) ?? 0;
            });
            CreateMap<ExamEntity, SearchDTO>();

            CreateMap<ExamUpdateDTO, ExamEntity>().ReverseMap();
        }
    }
}