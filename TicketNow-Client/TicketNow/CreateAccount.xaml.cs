using System;
using System.Collections.Generic;
using System.Net.Http;
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

            var request = new FormUrlEncodedContent(values);
            //URI
            HttpResponseMessage response = await client.PostAsync("http://ticket-now.ddns.net:5000/api/register", request);

            if(response.IsSuccessStatusCode) await DisplayAlert("", "Your account has been created", "Ok");

            else await DisplayAlert("Erro", "Invalid Entry", "Try Again");


        }
    }
}
