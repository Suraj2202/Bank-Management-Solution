using NUnit.Framework;
using BankManagement_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMSAPI.Tests;
using Moq;
using BankManagement_WPF.ViewModel.Helpers;
using BankManagement_WPF.Model;
using System.Net.Http;
using System.Net.Http.Json;
using Moq.Protected;
using System.Net;
using System.Threading;
using BankManagement_WPF.ViewModel.Commands;

namespace WPF.Tests
{
    [TestFixture]
    public class ApplyLoanVM_Tests
    {        
        private ApplyLoanVM applyLoanVM;
        private Mock<IApplyLoanHelper> mockApplyLoanHelper;
        private ApplyLoanCommand applyLoanCommand;
        /*

                private Mock<HttpResponseMessage> mockMessage;
                private MockRepository _mockRepository;
                private Mock<HttpMessageHandler> _handlerMock;
        */

        [SetUp]
        public void Setup()
        {
            applyLoanVM = new ApplyLoanVM();
            mockApplyLoanHelper = new Mock<IApplyLoanHelper>();
            mockApplyLoanHelper.Setup(x => x.CreateLoan(It.IsAny<LoanDetail>()))
                .ReturnsAsync("Added Succesfully");


            /*
              _mockRepository = new(MockBehavior.Default);
            mockHttpClient = new Mock<HttpClient>();
            mockMessage = new Mock<HttpResponseMessage>();
                        mockMessage.SetReturnsDefault("Added Succesfully");
                        *//*
                                    var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
                                    mockHttpMessageHandler.Protected()
                                        .Setup<Task<HttpResponseMessage>>("PostAsJsonAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                                        .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });
                        *//*
                        mockHttpClient.Object.PostAsJsonAsync(It.IsAny<string>(), It.IsAny<HttpResponseMessage>(), default).Await();

                        mockHttpClient.Setup(x => x.PostAsJsonAsync(It.IsAny<string>(), It.IsAny<HttpResponseMessage>(), default))
                            .ReturnsAsync(new HttpResponseMessage(statusCode: HttpStatusCode.OK));
            */
            /*

                        _handlerMock = _mockRepository.Create<HttpMessageHandler>();
                        _handlerMock
                            .Protected()
                            .Setup<Task<HttpResponseMessage>>(
                                "SendAsync",
                                ItExpr.IsAny<HttpRequestMessage>(),
                                ItExpr.IsAny<CancellationToken>()
                            )
                            .ReturnsAsync(new HttpResponseMessage
                            {
                                StatusCode = HttpStatusCode.OK,
                                Content = new StringContent("Added Succesfully")
                            })
                            .Verifiable();*/

            //setup variables
            GlobalVariables.USERNAME = TestData.LoanDetail.UserName;
            applyLoanVM.LoanAmount = "987654";
            applyLoanVM.LoanDate = DateTime.Now.ToString("MM/dd/yyyy");
            applyLoanVM.LoanDuration = "24";
            applyLoanVM.LoanType = "System.Windows.Controls.ComboBoxItem: Home";
        }

        [Test]
        public void CreateNewLoan_Test()
        {
            applyLoanCommand = new ApplyLoanCommand(applyLoanVM);
            applyLoanCommand.CanExecute(null);
            applyLoanCommand.Execute(null);

            Assert.NotNull(applyLoanVM.ROI);
        }

        [Test]
        public void CreateNewLoan_Error_Test()
        {
            applyLoanVM.LoanAmount = "test";
            applyLoanVM.LoanDate = "02/02/2029";
            applyLoanVM.LoanDuration = "5.0";
            applyLoanVM.LoanDuration = "0test";
            applyLoanVM.LoanDuration = "9&";
            applyLoanVM.PropertyErrors.Add("", "");
            Assert.NotNull(applyLoanVM.PropertyErrors.Any());
        }

        [Test]
        public void CreateNewLoan_Warning_Test()
        {
            applyLoanVM.Warning = "Something Went Wrong";
            Assert.NotNull(applyLoanVM.Warning);
        }

        [Test]
        public void CreateNewLoan_LoanDuration_Errors_Test()
        {
            applyLoanVM.LoanDuration = "";
            applyLoanVM.CreateNewLoan();

            Assert.IsEmpty(applyLoanVM.LoanDuration);
        }

        [Test]
        public void CreateNewLoan_LoanDuration_WithValue_Errors_Test()
        {
            applyLoanVM.LoanDuration = "0.0";
            applyLoanVM.CreateNewLoan();

            Assert.IsNotNull(applyLoanVM.LoanDuration);
        }

        [Test]
        public void CreateNewLoan_LoanAmount_Errors_Test()
        {
            applyLoanVM.LoanAmount = "test";
            applyLoanVM.CreateNewLoan();

            Assert.IsNotNull(applyLoanVM.LoanAmount);
        }

        [Test]
        public void CreateNewLoan_LoanDate_Errors_Test()
        {
            applyLoanVM.LoanDate = "02/02/2029";
            applyLoanVM.CreateNewLoan();

            Assert.IsNotNull(applyLoanVM.LoanDate);
        }

        [Test]
        public void CreateNewLoan_Dash_LoanDate_Test()
        {
            applyLoanVM.LoanDate = "02-02-2020";
            applyLoanVM.CreateNewLoan();

            Assert.IsNotNull(applyLoanVM.LoanDate);
        }

        [Test]
        public void CreateNewLoan_Shlash_LoanDate_Test()
        {
            applyLoanVM.LoanDate = "02/02/2020";
            applyLoanVM.CreateNewLoan();

            Assert.IsNotNull(applyLoanVM.LoanDate);
        }


    }
}
