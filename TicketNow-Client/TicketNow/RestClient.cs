using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TicketNow
{
    public class RestClient<T>
    {
        private string token = null;

        // This code matches the HTTP Request that we included in our api controller
        public async Task<string> checkLogin(string username, string password)
        {
            if ((username == "a11111" && password == "epah_mas_que_chatice"))
            {
                //token do dev: -depois do logout retirar ////
                token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE1ODgyNTE0MzMsIm5iZiI6MTU4ODI1MTQzMywianRpIjoiMjA0YmY2OGMtNDZmYy00NzUxLThhY2MtNzMwOGZjN2FkNGNiIiwiZXhwIjoxNjE5Nzg3NDMzLCJpZGVudGl0eSI6ImExMTExMSIsImZyZXNoIjpmYWxzZSwidHlwZSI6ImFjY2VzcyIsInVzZXJfY2xhaW1zIjp7InBlcm1pc3Npb25zIjoxfX0.1HPkWdG55joGPv6_fBZYXXmVa57An32csYHSxYYM0ZU";
            }

            
                if ((username == "a12345" && password == "epah_mas_que_chatice"))
                {
                    //token do dev: -depois do logout retirar ////
                    token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE1ODgyNTE1ODAsIm5iZiI6MTU4ODI1MTU4MCwianRpIjoiMzViMTk5NjgtMTg1MS00Y2JiLTkxN2UtNjczNWUzYTkzNDQ1IiwiZXhwIjoxNjE5Nzg3NTgwLCJpZGVudGl0eSI6ImExMjM0NSIsImZyZXNoIjpmYWxzZSwidHlwZSI6ImFjY2VzcyIsInVzZXJfY2xhaW1zIjp7InBlcm1pc3Npb25zIjo1fX0._-ZUM1O5Iy6f-N8f7WjCti8n0JGIfH6jt4ejpdvwBmQ";
                }

                else
                {

                    var client = new HttpClient();
                    var byteArray = Encoding.ASCII.GetBytes(username + ":" + password);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    //test with server
                    HttpResponseMessage response = await client.GetAsync("http://ticketnow.ddns.net:5000/api/login");
                    HttpContent content = response.Content;

                    //Read the string.
                    string result = await content.ReadAsStringAsync();

                    if (result != null &&  result.Length >= 50)
                    {

                        JObject jdynamic = JsonConvert.DeserializeObject<dynamic>(result);
                        var accessToken = jdynamic.Value<string>("token");

                        //get token
                        token = accessToken;
                    }
                }
            
            return token;
        }


    }
}

