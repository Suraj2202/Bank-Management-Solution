using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSecurity.Models.DTO
{
    public class LoanDetailDTO
    {
        public int LoanId { get; set; }
        public string UserName { get; set; }
        public string LoanType { get; set; }
        public double LoanAmount { get; set; }
        public DateTime LoanDate { get; set; }
        public float RateOfInterest { get; set; }
        public int LoanDuration { get; set; }
        public string Status { get; set; }
    }
}
