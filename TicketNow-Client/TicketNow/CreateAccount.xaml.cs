using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class CreateAccount : ContentPage
    {
        public CreateAccount()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void onCreateAccountButtonClicked(object sender, EventArgs args)
        {

            //POST
            var client = new HttpClient();

            //test with server

            //PARAMETERS
            var values = new Dictionary<string, string>
            {
               { "id_user", username.Text},
               { "email", email.Text },
               { "password", password.Text },
               { "name", name.Text }
            };

            if (String.IsNullOrWhiteSpace(username.Text))
            {
                await DisplayAlert("", "Insuficient arguments", "Try Again");
                return;
            }

            if (String.IsNullOrWhiteSpace(email.Text))
            {
                await DisplayAlert("", "Insuficient arguments", "Try Again");
                return;
            }

            if (String.IsNullOrWhiteSpace(password.Text))
            {
                await DisplayAlert("", "Insuficient arguments", "Try Again");
                return;
            }

            if (String.IsNullOrWhiteSpace(name.Text))
            {
                await DisplayAlert("", "Insuficient arguments", "Try Again");
                return;
            }


            var request = new FormUrlEncodedContent(values);
            //URI
            HttpResponseMessage response = await client.PostAsync("http://ticket-now.ddns.net:5000/api/register", request);

            
            string r = await response.Content.ReadAsStringAsync();


            JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(r);

            string s = jObject.Value<string>("error");

            if (response.IsSuccessStatusCode) await DisplayAlert("", "Your account has been created", "Ok");

            else if (s.Equals("User already exists"))
            {
                await DisplayAlert("", "Username already exists", "Try Again");
            }
            else if (s.Equals("Insuficient arguments"))
            {
                await DisplayAlert("", "Insuficient arguments", "Try Again");
            }
            else await DisplayAlert("Erro", "Invalid Entry: The Name can't have numbers, and both the Password and the User ID have to start with a letter", "Try Again");


        }
    }
}
