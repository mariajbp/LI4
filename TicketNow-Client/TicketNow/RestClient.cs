using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace TicketNow
{
    public class RestClient<T>
    {
        private string token = null;

        // This code matches the HTTP Request that we included in our api controller
        public async Task<string> checkLogin(string username, string password)
        {
          //  if (username == "a11111" && password == "epah_mas_que_chatice") token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE1ODgyNTE0MzMsIm5iZiI6MTU4ODI1MTQzMywianRpIjoiMjA0YmY2OGMtNDZmYy00NzUxLThhY2MtNzMwOGZjN2FkNGNiIiwiZXhwIjoxNjE5Nzg3NDMzLCJpZGVudGl0eSI6ImExMTExMSIsImZyZXNoIjpmYWxzZSwidHlwZSI6ImFjY2VzcyIsInVzZXJfY2xhaW1zIjp7InBlcm1pc3Npb25zIjoxfX0.1HPkWdG55joGPv6_fBZYXXmVa57An32csYHSxYYM0ZU";
          //  else
          //  {
                var client = new HttpClient();

                //verificar se esta correto
                var byteArray = Encoding.ASCII.GetBytes(username + ":" + password);

                //verificar se está correto
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                //PARAMETERS
                var values = new Dictionary<string, string>
               {
               { "Username", username },
               { "Password", password }
               };

                var request = new FormUrlEncodedContent(values);
                //URI
                HttpResponseMessage response = await client.PostAsync("http://ticketnow.ddns.net:5000/api/login", request);

                HttpContent content = response.Content;

                //Read the string.
                string result = await content.ReadAsStringAsync();

                if (result != null && result.Length >= 50)
                {

                    JObject jdynamic = JsonConvert.DeserializeObject<dynamic>(result);
                    var accessToken = jdynamic.Value<string>("token");

                    //get token
                    token = accessToken;
                }
          //  }
            
            return token;
        }

       

    }
}

