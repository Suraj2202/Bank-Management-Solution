using LoginSecurity.Data;
using LoginSecurity.Models.Domains;
using LoginSecurity.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMSAPI.Tests
{
    [TestFixture]
    public class ApplyLoanRepositryTests
    {
        private List<LoanDetail> loanDetail_list = new List<LoanDetail>() { TestData.LoanDetail };
        private Mock<BankManagementDbContext> mockBankDbContext;
        private Mock<DbSet<LoanDetail>> mockLoanDetailDbSet;
        private IApplyLoanRepositry applyLoanRepositry;

        public ApplyLoanRepositryTests()
        {           
        }

        [SetUp]
        public void Setup()
        {
            var data = loanDetail_list.AsQueryable();
            mockBankDbContext = new Mock<BankManagementDbContext>();
            mockLoanDetailDbSet = new Mock<DbSet<LoanDetail>>();
            mockLoanDetailDbSet.As<IQueryable<LoanDetail>>().Setup(m => m.Provider).Returns(data.Provider);
            mockLoanDetailDbSet.As<IQueryable<LoanDetail>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockLoanDetailDbSet.As<IQueryable<LoanDetail>>().Setup(m => m.Expression).Returns(data.Expression);
            mockLoanDetailDbSet.As<IQueryable<LoanDetail>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockBankDbContext.Setup(x => x.LoanDetails).Returns(mockLoanDetailDbSet.Object);
            applyLoanRepositry = new ApplyLoanRepositry(mockBankDbContext.Object);
        }

        [Test]
        public void GetLoanAsync_Test()
        {
            int loanId = loanDetail_list.FirstOrDefault().LoanId;

            var res = applyLoanRepositry.GetLoanAsync(loanId);

            Assert.IsTrue(res.IsCompleted);
        }

        [Test]
        public void GetAllLoanAsync_Test()
        {
            string username = loanDetail_list.FirstOrDefault().UserName;

            var res = applyLoanRepositry.GetAllLoanAsync(username);

            Assert.IsTrue(res.IsCompleted);
        }


        //Task<LoanDetail> GetLoanAsync(int loanId);
        //Task<List<LoanDetail>> GetAllLoanAsync(string userName);
        //Task<List<LoanDetail>> GetAllAdminLoanAsync();
        //Task<bool> SaveLoanDeatilAsync(LoanDetail loanDetail);
        //Task<bool> UpdateLoanDeatilAsync(int loanId, LoanDetail loanDetail);
        //Task<bool> UpdateLoanStatusAsync(int loanId, string status);
        //Task<bool> UpdateLoanCommentAsync(int loanId, string comment);

    }
}