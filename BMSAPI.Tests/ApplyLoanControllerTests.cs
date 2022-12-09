using AutoMapper;
using LoginSecurity.Controllers.LoanApply;
using LoginSecurity.Models.Domains;
using LoginSecurity.Models.DTO;
using LoginSecurity.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;

namespace BMSAPI.Tests
{
    [TestFixture]
    class ApplyLoanControllerTests
    {
        private Mock<IApplyLoanRepositry> applyLoanRepository;
        private ApplyLoanController applyLoanController;
        //private readonly IMapper mapper;
        private LoanDetail loanDetail_one;
        private Mock<IMapper> mapper;

        public ApplyLoanControllerTests()
        {
            loanDetail_one = TestData.LoanDetail;

        }

        [SetUp]
        public void Setup()
        {
            applyLoanRepository = new Mock<IApplyLoanRepositry>();
            mapper = new Mock<IMapper>();
            applyLoanController = new ApplyLoanController(applyLoanRepository.Object, mapper.Object);
        }

        [Test]
        public void CallRequest_VerifyGetByIdInvoked()
        {
            applyLoanRepository.Setup(x => x.GetLoanAsync(It.IsAny<int>()))
                .ReturnsAsync(new LoanDetail()
                {
                    LoanId = loanDetail_one.LoanId,
                    LoanAmount = loanDetail_one.LoanAmount,
                    LoanDate = loanDetail_one.LoanDate,
                    LoanDuration = loanDetail_one.LoanDuration,
                    LoanType = loanDetail_one.LoanType,
                    Comment = loanDetail_one.Comment,
                    RateOfInterest = loanDetail_one.RateOfInterest,
                    Status = loanDetail_one.Status,
                    UserName = loanDetail_one.UserName

                });

            mapper.Setup(x => x.Map<LoanDetail, LoanDetailDTO>(It.IsAny<LoanDetail>()))
                .Returns((LoanDetail loan)=>new LoanDetailDTO{ LoanId = loan.LoanId});

            _ = applyLoanController.Get(loanDetail_one.LoanId);
            applyLoanRepository.Verify(x => x.GetLoanAsync(loanDetail_one.LoanId), Times.Once);
        }

        [Test]
        public void CallRequest_VerifyPost_Success()
        {
            applyLoanRepository.Setup(x => x.SaveLoanDeatilAsync(It.IsAny<LoanDetail>()))
                .ReturnsAsync(true);

            var res = applyLoanController.Post(loanDetail_one);

            Assert.AreEqual(true, res.IsCompleted);

        }
    }
}
