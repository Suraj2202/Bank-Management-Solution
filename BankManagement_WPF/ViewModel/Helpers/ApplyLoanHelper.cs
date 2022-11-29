using BankManagement_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_WPF.ViewModel.Helpers
{
    class ApplyLoanHelper
    {
        public const string BASE_URL = "http://localhost:7001/api/";
        public const string POST_URL = "ApplyLoan";

        public static string CreateLoan(LoanDetail loanDetail)
        {
            string agent ="";
            string URL = BASE_URL + POST_URL;

            using (HttpClient httpClient = new HttpClient())
            {
              httpClient.PostAsJsonAsync(URL, loanDetail, default).ToObservable()
                    .Subscribe(response =>
                    {
                        agent =  response.Content.ReadAsStringAsync().ToString();
                    });
            }
            return agent;
        }
    }
}
