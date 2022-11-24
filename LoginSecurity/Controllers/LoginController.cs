using AutoMapper;
using LoginSecurity.Models.Domains;
using LoginSecurity.Models.DTO;
using LoginSecurity.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginSecurity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public LoginController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        //Getting User Details

        [HttpGet("{uname}")]
        public async Task<UserDetailDTO> Get(string uname)
        {
            UserDetail userDetail = await userRepository.GetUserAsync(uname);
            UserDetailDTO userDetailDTO = mapper.Map<UserDetailDTO>(userDetail);

            if(userDetailDTO != null)
                userDetailDTO.Token = null;

            return userDetailDTO;
        }

        //Validate User

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginDetailDTO value)
        {
            int role = await userRepository.ValidateUserCredAsync(value.UserName, value.Password);

            if (role == 0)
                return Ok("User");
            else if (role == 1)
                return Ok("Admin");
            else
                return NotFound("User Not Found");
            
        }

    }
}
