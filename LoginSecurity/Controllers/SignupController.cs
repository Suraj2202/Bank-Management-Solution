using AutoMapper;
using LoginSecurity.Models.Domains;
using LoginSecurity.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginSecurity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public SignupController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }


        // POST api/<SignupController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDetail value)
        {
            bool response = await userRepository.SaveUserDeatilAsync(value);
            
            if(response)
                return Ok("Added Succesfully");
            
            return BadRequest("User Already Exists");
            
        }

    }
}
