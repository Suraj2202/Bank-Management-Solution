using AutoMapper;
using LoanApply.Models.Domains;
using LoanApply.Models.DTO;
using LoanApply.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoanApply.Controllers
{
    [ApiController]
    public class ApplyLoanController : ControllerBase
    {
        private readonly IApplyLoanRepositry userRepository;
        private readonly IMapper mapper;

        public ApplyLoanController(IApplyLoanRepositry userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        // GET: api/<ApplyLoanController>
        [Route("api/[controller]/{loanId}")]
        [HttpGet]
        public async Task<LoanDetailDTO> Get(int loanId)
        {
            LoanDetail loanDetail = await userRepository.GetLoanAsync(loanId);
            LoanDetailDTO loanDetailDTO = mapper.Map<LoanDetailDTO>(loanDetail);
            
            return loanDetailDTO;
        }

        // GET: api/<ApplyLoanController>
        [Route("api/[controller]/all/{userName}")]
        [HttpGet]
        public async Task<List<LoanDetailDTO>> Get(string userName)
        {
            List<LoanDetail> loanDetails = await userRepository.GetAllLoanAsync(userName);
            List<LoanDetailDTO> loanDetailDTO = mapper.Map<List<LoanDetailDTO>>(loanDetails);

            return loanDetailDTO;
        }

        // POST api/<ApplyLoanController>
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> Post([FromBody] LoanDetail value)
        {
            bool response = await userRepository.SaveLoanDeatilAsync(value);

            if (response)
                return Ok("Added Succesfully");

            return BadRequest("Something Went Wrong");

        }

        // PUT api/<ApplyLoanController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApplyLoanController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
