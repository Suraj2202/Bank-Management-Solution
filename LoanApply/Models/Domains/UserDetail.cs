using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoanApply.Models.Domains
{
    public class UserDetail
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public int Role { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public int PAN { get; set; }
        public int Contact { get; set; }
        public DateTime DOB { get; set; }
        public string AccountType { get; set; }


        //Navigation Properties
        public IEnumerable<LoanDetail> LoanDetails { get; set; }
    }
}
