using AutoMapper;
using LoginSecurity.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSecurity.Controllers
{

    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public SessionController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("api/Validate/{uname}")]
        public async Task<bool> Get(string uname)
        {
            bool sessionValidate = await userRepository.ValidateUserSessionAsync(uname);
            return sessionValidate;
        }


        [Route("api/Logout")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string uname)
        {
            bool response = await userRepository.EndSessionAsync(uname);

            if (response)
                return Ok("Logot Succesfully");

            return BadRequest("Bad Request");

        }
    }
}
