using BankManagement_WPF.Model;
using BankManagement_WPF.View;
using BankManagement_WPF.ViewModel.Commands;
using BankManagement_WPF.ViewModel.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BankManagement_WPF.ViewModel
{
    class ApplyLoanVM : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private Dictionary<string, string> propertyErrors;
        TextBlockValidation textBlockValidation;

        #region Value Property
        public Dictionary<string, string> PropertyErrors
        {
            get { return propertyErrors; }
            set { propertyErrors = value; }
        }

        private string loanType;

        public string LoanType
        {
            get { return loanType; }
            set 
            { 
                loanType = value;
                OnPropertyChanged("LoanType");
            }
        }

        private string loanAmount;

        public string LoanAmount
        {
            get { return loanAmount; }
            set 
            { 
                loanAmount = value;
                OnPropertyChanged("LoanAmount");

                ClearErrors(nameof(LoanAmount));
                if (LoanAmount.Any(char.IsLetter) || LoanAmount.Any(char.IsSymbol) || LoanAmount.Any(char.IsPunctuation))
                    AddError(nameof(LoanAmount), "Loan Amount should be numbers only");
            }
        }

        private string loanDate;

        public string LoanDate
        {
            get { return loanDate; }
            set 
            { 
                loanDate = value; 
                OnPropertyChanged("LoanDate");

                ClearErrors(nameof(LoanDate));
                bool res = textBlockValidation.FutureDateValidation(value);
                if (res)
                    AddError(nameof(LoanDate), "No Future Date Please.");

            }
        }

        private string roi;

        public string ROI
        {
            get { return roi; }
            set { roi = value; OnPropertyChanged("ROI"); }
        }

        private string loanDuration;

        public string LoanDuration
        {
            get { return loanDuration; }
            set
            { 
                loanDuration = value;
                float duration = float.Parse(LoanDuration);
                ROI = (duration / 12).ToString(); 
                OnPropertyChanged("LoanDuration");

                ClearErrors(nameof(LoanDuration));
                if (LoanDuration.Any(char.IsLetter) || LoanDuration.Any(char.IsSymbol) || LoanDuration.Any(char.IsPunctuation))
                    AddError(nameof(LoanDuration), "Loan Duration should be numbers only");
            }
        }

        private string warning;

        public string Warning
        {
            get { return warning; }
            set { warning = value; OnPropertyChanged("Warning"); }
        }

        #endregion

        public ApplyLoanCommand ApplyLoanCommand { get; set; }
        public PreviousAppliedLoansCommand PreviousAppliedLoansCommand { get; set; }

        public ApplyLoanVM()
        {
            propertyErrors = new Dictionary<string, string>();
            textBlockValidation = new TextBlockValidation();
            //Session Check
            LoanDate = DateTime.Today.ToString("MM/dd/yyyy").Replace('-','/');
            ROI = "0";
            PreviousAppliedLoansCommand = new PreviousAppliedLoansCommand(this);
            ApplyLoanCommand = new ApplyLoanCommand(this);
        }

        public void PreviousAppliedLoanWindowOpen()
        {
            PreviousAppliedLoansWindow loansWindow = new PreviousAppliedLoansWindow();
            loansWindow.ShowDialog();
        }

        public async void CreateNewLoan()
        {
            //validation
            CheckForIsNullOrEmpty(nameof(LoanAmount), LoanAmount);
            CheckForIsNullOrEmpty(nameof(LoanDate), LoanDate);
            CheckForIsNullOrEmpty(nameof(LoanDuration), LoanDuration);

            if (string.IsNullOrEmpty(LoanType) || string.IsNullOrEmpty(LoanAmount) || string.IsNullOrEmpty(LoanDate) || string.IsNullOrEmpty(LoanDuration))
            {
                Warning = "All fields are mandatory";
                return;
            }

            if(LoanAmount.Any(char.IsLetter) || LoanAmount.Any(char.IsSymbol) || LoanAmount.Any(char.IsPunctuation))
            {
                Warning = "Loan Amount should be numbers only";
                return;
            }

            if (textBlockValidation.FutureDateValidation(LoanDate))
            {
                Warning = "No Future Date Please.";
                return;
            }

            if (LoanDuration.Any(char.IsLetter) || LoanDuration.Any(char.IsSymbol) || LoanDuration.Any(char.IsPunctuation))
            {
                Warning = "Loan Duration should be numbers only";
                return;
            }

            float duration = float.Parse(LoanDuration);
            ROI = (duration/12).ToString();

            string val = LoanDate.Contains("-") ? "-" : "/";
            string[] dates = LoanDate.Split(" ")[0].Split(val);
            string myDate = dates[1] + "/" + dates[0] + "/" + dates[2];

            LoanDetail loan = new LoanDetail()
            {
                UserName = GlobalVariables.USERNAME,
                LoanType = LoanType.Split(":")[1].Trim(),
                LoanDate = DateTime.Parse(myDate),
                LoanAmount = double.Parse(LoanAmount),
                LoanDuration = int.Parse(LoanDuration),
                RateOfInterest = float.Parse(ROI),
                Status = "Pending"
            };

            IApplyLoanHelper loanHelper = new ApplyLoanHelper();
            string status =  await loanHelper.CreateLoan(loan);

            Warning = status;
        }       

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Error Handling

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors => propertyErrors.Any();

        public IEnumerable GetErrors(string propertyName)
        {
            return propertyErrors.GetValueOrDefault(propertyName, "");
        }

        public void AddError(string propertyName, string errorMessage)
        {
            if (!propertyErrors.ContainsKey(propertyName))
            {
                propertyErrors.Add(propertyName, errorMessage);
                OnPropertyChanged("PropertyErrors");
            }
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertName));
        }

        private void ClearErrors(string propertyName)
        {
            propertyErrors?.Remove(propertyName);
            OnPropertyChanged("PropertyErrors");
            OnErrorsChanged(propertyName);
        }

        private void CheckForIsNullOrEmpty(string propertyName, string propertyValue)
        {
            if (string.IsNullOrEmpty(propertyValue))
            {
                ClearErrors(propertyName);
                AddError(propertyName, propertyName + " Field is mandatory.");
            }
        }

        #endregion
    }
}
