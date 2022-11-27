using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankManagement_WPF.Validations
{
    public class TextBlockValidation
    {
        string[] list = new[] { "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "+", "=", "\"" };
        public bool UserNameValidation(string userName)
        {            
            return list.Any(userName.Contains);
        }

        public bool PasswordValidation(string password)
        {

            return password.Length >= 8 &&
                     password.Length <= 15 &&
                     password.Any(char.IsLetter) &&
                    (password.Any(char.IsSymbol) || password.Any(char.IsPunctuation));
            
        }

        public bool PANandContactNoValidation(string Pan)
        {
            return Pan.Length == 10 &&
                Pan.All(char.IsDigit) && Pan[0]!='0';
        }

        public bool EmailIDValidation(string emailId)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(emailId);
        }

        public bool FutureDateValidation(string date)
        {
            string val = date.Contains("-") ? "-" : "/";
            string[] dates = date.Split(" ")[0].Split(val);
            string myDate = dates[1] + "-" + dates[0] + "-" + dates[2];

            DateTime now = DateTime.Now.Date;
            DateTime selectedDate = DateTime.Parse(myDate);
            int res = DateTime.Compare(now, selectedDate);

            if (res < 0)
                return true;
            else
                return false;
        }

        public bool AgeGreaterThan18(string date)
        {
            string val = date.Contains("-") ? "-" : "/";
            string[] dates = date.Split(" ")[0].Split(val);
            string myDate = dates[1] + "-" + dates[0] + "-" + dates[2];

            DateTime now = DateTime.Now.Date;
            DateTime selectedDate = DateTime.Parse(myDate);
            int age = now.Year - selectedDate.Year;

            if(age >= 18)
            {
                return false;
            }
            return true;
        }
    }
}
