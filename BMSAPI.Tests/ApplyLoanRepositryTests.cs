using LoginSecurity.Data;
using LoginSecurity.Models.Domains;
using LoginSecurity.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace BMSAPI.Tests
{
    [TestFixture]
    public class ApplyLoanRepositryTests
    {
        private LoanDetail loanDetail_one;
        private LoanDetail loanDetail_two;

        public ApplyLoanRepositryTests()
        {
            loanDetail_one = new LoanDetail()
            {
                LoanId = 1,
                LoanAmount = 100000,
                LoanDate = new DateTime(2022,1,1),
                LoanDuration = 6,
                LoanType = "Car",
                Comment = "",
                RateOfInterest = 10,
                Status = "Pending",
                UserName = "test"                
            };

            loanDetail_two = new LoanDetail()
            {
                LoanId = 2,
                LoanAmount = 100500,
                LoanDate = new DateTime(2022, 10, 1),
                LoanDuration = 6,
                LoanType = "Home",
                Comment = "",
                RateOfInterest = 10,
                Status = "Pending",
                UserName = "test"
            };

        }

        [Test]
        public void SaveLoanDeatilAsync_Success_CheckValueFromDb()
        {
            //arrange
            var options = new DbContextOptionsBuilder<BankManagementDbContext>()
                .UseInMemoryDatabase("tempLoan").Options;

            //act
            using (var context = new BankManagementDbContext(options))
            {
                var repo = new ApplyLoanRepositry(context);
                _ = repo.SaveLoanDeatilAsync(loanDetail_one);
            }

            //assert
            using (var context = new BankManagementDbContext(options))
            {
                var loanFromDb = context.LoanDetails.FirstOrDefault(x => x.LoanId == loanDetail_one.LoanId);
                Assert.AreEqual(loanDetail_one.LoanId, loanFromDb.LoanId);
                Assert.AreEqual(loanDetail_one.LoanAmount, loanFromDb.LoanAmount);
                Assert.AreEqual(loanDetail_one.LoanDate, loanFromDb.LoanDate);
                Assert.AreEqual(loanDetail_one.LoanDuration, loanFromDb.LoanDuration);
                Assert.AreEqual(loanDetail_one.LoanType, loanFromDb.LoanType);
                Assert.AreEqual(loanDetail_one.UserName, loanFromDb.UserName);
            }
        }

        [Test]
        public void SaveLoanDeatilAsync_Fail_CheckValueFromDb()
        {
            //arrange
            var options = new DbContextOptionsBuilder<BankManagementDbContext>()
                .UseInMemoryDatabase("tempLoan").Options;

            //act
            using (var context = new BankManagementDbContext(options))
            {
                var repo = new ApplyLoanRepositry(context);
                _ = repo.SaveLoanDeatilAsync(loanDetail_one);
            }

            //assert
            using (var context = new BankManagementDbContext(options))
            {
                var loanFromDb = context.LoanDetails.FirstOrDefault(x => x.LoanId == loanDetail_one.LoanId);
                Assert.AreEqual(loanDetail_one.LoanId, loanFromDb.LoanId);
                Assert.AreEqual(loanDetail_one.LoanAmount, loanFromDb.LoanAmount);
                Assert.AreEqual(loanDetail_one.LoanDate, loanFromDb.LoanDate);
                Assert.AreEqual(loanDetail_one.LoanDuration, loanFromDb.LoanDuration);
                Assert.AreEqual(loanDetail_one.LoanType, loanFromDb.LoanType);
                Assert.AreEqual(loanDetail_one.UserName, loanFromDb.UserName);
            }
        }
    }
}