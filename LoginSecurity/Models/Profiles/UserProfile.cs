using AutoMapper;
using LoginSecurity.Models.Domains;
using LoginSecurity.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSecurity.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDetail, UserDetailDTO>().ReverseMap();
        }
    }
}
