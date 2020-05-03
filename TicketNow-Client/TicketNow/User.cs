using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TicketNow
{
    public class User
    {
        
        public string id_user { get; set; }
        public string email { get;  set; }
        public string name { get; set; }
        public IList<Ticket> owned_tickets { get; set; }
        public int permissoes; //admin:3 user:1
        public int sm=0; //simple meal
        public int cm=0;  //complete meal
        

       
          //login with token and set information and owned_tickets
        public async Task<bool> setInfo(string token, string id_userr)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //LOGIN
            //test with server

            HttpResponseMessage response = await client.GetAsync("http://ticketnow.ddns.net:5000/api/user/"+ id_userr);
            HttpContent content = response.Content;

            //Read the string.
            string result = await content.ReadAsStringAsync();

           

            JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);

            var user = jObject.SelectToken("user");

            
            this.id_user = user.Value<string>("id_user");
            this.email = user.Value<string>("email");
            this.name = user.Value<string>("name");

            if (this.id_user == "a12345") this.permissoes = 3;
            else if (this.id_user == "a11111") this.permissoes = 1;

                //OWNED_TICKETS
                //test with server
                HttpResponseMessage resp = await client.GetAsync("http://ticketnow.ddns.net:5000/api/user/"+id_userr+"/tickets");
            HttpContent cont = resp.Content;

            //Read the string.
            string r = await cont.ReadAsStringAsync();

           

            JObject tickets = Newtonsoft.Json.Linq.JObject.Parse(r);

            
            JArray a = (JArray)tickets["owned_tickets"];

           IList<Ticket> ticket = a.ToObject<IList<Ticket>>();

            this.owned_tickets = ticket;

            //list of tickets (number of tickets)
            foreach(var t in ticket)
            {
                if (t.type == 1) this.sm = this.sm + 1;
                else this.cm = this.cm + 1;
            }

            return true;

        }


       

    }

}


