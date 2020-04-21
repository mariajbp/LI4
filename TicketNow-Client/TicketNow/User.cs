using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace TicketNow
{
    public class User
    {
        public string id_user { get; set; }
        public string email { get;  set; }
        public string password { get; set; }
        public string name { get; set; }
        public int cm = 12;  //complete meal
        public int sm = 0; //simple meal

       
          //login with token and set information
        public async Task<bool> setInfo(string token, string id_userr)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //test with server
            HttpResponseMessage response = await client.GetAsync("http://ticketnow.ddns.net:5000/api/user?id_user=" + id_userr);
            HttpContent content = response.Content;

            //Read the string.
            string result = await content.ReadAsStringAsync();

            Console.WriteLine(result);

            JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);

            var user = jObject.SelectToken("user");
            this.id_user = user.Value<string>("id_user");
            this.email = user.Value<string>("email");
            this.name = user.Value<string>("name");
            this.password = "epah_mas_que_chatice"; //apenas para testar
            return true;

        }

        public bool changePass(string oldpass)
        {
            bool r;
            if (this.password == oldpass) r = true;
            else r= false;
            return r;
            

        }

        public bool matchPass(string newpass, string newpass2)
        {
            bool r;
            if (newpass == newpass2) r = true;
            else r = false;
            return r;

      }


    }

}


