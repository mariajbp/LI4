using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TicketNow
{
    public class User
    {
        public string id_user { get; private set; }
        public string email { get; private set; }
        public string password { get; set; }
        public string name { get; private set; }
        public int cm = 12;  //complete meal
        public int sm = 0; //simple meal

public User(string token, string id_user)
        {
            getInfo(token, id_user);

           
        }

        private async void getInfo(string token, string id_userr)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //to test with localhost, change the next ip(192.168.43.42) to your pc local ip
            HttpResponseMessage response = await client.GetAsync("http://192.168.43.42:5000/api/user?id_user=" + id_userr);
            HttpContent content = response.Content;

            //Read the string.
            string result = await content.ReadAsStringAsync();

            Console.WriteLine(result);
           
            JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);

            var user = jObject.SelectToken("user");
            this.id_user = user.Value<string>("id_user");
            this.email = user.Value<string>("email");
            this.name = user.Value<string>("name");
            //not completed yet
            
        }

       
    }
}


