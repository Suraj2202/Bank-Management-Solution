using LoginSecurity.Models.Domains;
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

        public LoginController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        //Getting User Details

        [HttpGet("{uname}")]
        public UserDetail Get(string uname)
        {
            return userRepository.GetUser(uname);
        }

        //Validate User

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDetail value)
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
