using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Android.OS;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class DeleteUser : ContentPage
    {
        private string token;
        private long LastButtonClickTime = 0;

        public DeleteUser(string token)
        {

            this.token = token;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void onDeleteUserClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            if (String.IsNullOrWhiteSpace(USERNAME.Text))
            {
                await DisplayAlert("", "Invalid Parameter", "Try Again");
                return;
            }

            bool res = await this.deleteUser(USERNAME.Text, token);
           
            if (res)
                {
                    await DisplayAlert("", "User deleted with success", "Ok");
                }
                
                else await DisplayAlert("", "User does not exist", "Try Again");
           
        }


        public async Task<bool> deleteUser(string id_user, string accessToken)
        {

            //DELETE
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


            //URI
            HttpResponseMessage response = await client.DeleteAsync("http://ticket-now.ddns.net:5000/api/user?id_user="+id_user);

            string r = await response.Content.ReadAsStringAsync();
            Console.Write(r);

            JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(r);

            var data = jObject.Children();
            string datee =" ";
            foreach (var q in data)
            {

                JProperty jProperty = q.ToObject<JProperty>();
                datee = jProperty.Name;
            }

                if (datee.Equals("message"))
            {
                return true;
            }

            else return false;
        }

    }

}