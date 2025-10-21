using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ExamProject.Application.DTOs.AccountDTOs;
using ExamProject.Domain.Entities;

namespace ExamProject.Application.MappingConfig
{
    public class AuthenticationMapping : Profile
    {
        public AuthenticationMapping() 
        {
            CreateMap<RegisterUserDto, ApplicationUser>().AfterMap((src, dist) =>
            {
                dist.PasswordHash = src.Password;

            });
        }
    }
}
