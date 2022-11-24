using AutoMapper;
using LoanApply.Models.Domains;
using LoanApply.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanApply.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LoanDetail, LoanDetailDTO>().ReverseMap();
        }
    }
}
