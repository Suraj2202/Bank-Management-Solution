using BankManagement_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_WPF.ViewModel.Helpers
{
    class UpdateDetailHelper
    {
        public const string BASE_URL = "http://localhost:7001/api/";
        public const string LOANSTATUS_PUT_URL = "Update/status/{0}";
        public const string LOANCOMMENT_PUT_URL = "Update/comment/{0}";
        public const string USER_PUT_URL = "Update/user/{0}";



        public static async Task<string> UpdateLoanStatus(int loanId, string statusValue)
        {
            string agent;
            string URL = BASE_URL + string.Format(LOANSTATUS_PUT_URL, loanId);

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsJsonAsync(URL, statusValue, default);
                var json = await response.Content.ReadAsStringAsync();
                agent = json.ToString();
            }
            return agent;
        }

        public static async Task<string> UpdateUserDetail(string username, UserDetail userDetail)
        {
            string agent;
            string URL = BASE_URL + string.Format(USER_PUT_URL, username);

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsJsonAsync(URL, userDetail, default);
                var json = await response.Content.ReadAsStringAsync();
                agent = json.ToString();
            }
            return agent;
        }

        public static async Task<string> UpdateLoanComment(int loanId, string commentValue)
        {
            string agent;
            string URL = BASE_URL + string.Format(LOANCOMMENT_PUT_URL, (long)loanId);

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsJsonAsync(URL, commentValue, default);
                var json = await response.Content.ReadAsStringAsync();
                agent = json.ToString();
            }
            return agent;
        }
    }
}
