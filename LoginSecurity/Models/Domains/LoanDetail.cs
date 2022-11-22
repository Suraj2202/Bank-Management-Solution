using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSecurity.Models.Domains
{
    public class LoanDetail
    {
        [Key]
        public int LoanId { get; set; }
        public string UserName { get; set; }
        public string LoanType { get; set; }
        public double LoanAmount { get; set; }
        public DateTime LoanDate { get; set; }
        public float RateOfInterest { get; set; }
        public int LoanDuration { get; set; }


    }
}
