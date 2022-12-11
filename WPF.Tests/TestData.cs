using BankManagement_WPF.Model;
using BankManagement_WPF.Model.RequestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Tests
{
    public class TestData
    {
        private static UserDetail userDetail;

        public static UserDetail UserDetail
        {
            get 
            {
                userDetail = new UserDetail()
                {
                    UserName = "test",
                    Password = "test@121",
                    Token = "Logout",
                    Role = 0,
                    Name = "Test",
                    Address = "DC/4",
                    State = "MH",
                    Country = "IN",
                    Email = "test@test.com",
                    PAN = 9876543211,
                    Contact = 9879876543,
                    DOB = DateTime.Now.AddYears(-18),
                    AccountType = "Saving"
                };
                return userDetail; 
            }
        }

        private static LoanDetail loanDetail;

        public static LoanDetail LoanDetail
        {
            get 
            {
                loanDetail = new LoanDetail()
                {
                    LoanId = 1,
                    LoanAmount = 100000,
                    LoanDate = new DateTime(2022, 1, 1),
                    LoanDuration = 6,
                    LoanType = "Car",
                    Comment = "",
                    RateOfInterest = 10,
                    Status = "Pending",
                    UserName = "test"
                }; return loanDetail; 
            }
        }

        private static LoginDetail loginDetail;

        public static LoginDetail LoginDetail
        {
            get 
            {
                loginDetail = new LoginDetail()
                {
                    UserName = "test",
                    Password = "Test@121"
                }; 
                return loginDetail; 
            }
        }



    }
}
