using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_WPF.ViewModel.Helpers
{
    class SessionHelper
    {
        public const string BASE_URL = "http://localhost:7001/api/";
        public const string GET_URL = "Validate/{0}";
        public const string POST_URL = "Logout";

        public static async Task<bool> ValidateToken(string userName)
        {
            bool userDetail;

            string URL = BASE_URL + string.Format(GET_URL, userName);

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(URL);
                string json = await response.Content.ReadAsStringAsync();
                userDetail = JsonConvert.DeserializeObject<bool>(json);
            };

            return userDetail;
        }

        public static async Task<string> LogoutUser(string userName)
        {
            string agent;
            string URL = BASE_URL + POST_URL;

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync(URL, userName, default);
                var json = await response.Content.ReadAsStringAsync();
                agent = json.ToString();
            }
            return agent;
        }
    }
}
