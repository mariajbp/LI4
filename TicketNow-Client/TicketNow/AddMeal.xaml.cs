using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Android.OS;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class AddMeal : ContentPage
    {
        private User u;
        private string token;
        private long LastButtonClickTime = 0;
        public AddMeal(User u, string token)
        {
            this.u = u;
            this.token = token;

            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }


        private async void onAddMealClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();

            bool res = await this.addMeal(date.Text, mealtype.Text, location.Text,soup.Text,maindish.Text,description.Text, token);
            if (res)
            {
                await DisplayAlert("", "Success", "Ok");
            }

        }


        public async Task<bool> addMeal(string date, string mealtype, string location,string soup, string maindish, string description, string accessToken)
        {

            //POST
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            //PARAMETERS
            var values = new Dictionary<string, string>
            {
               { "date", date },
               { "meal_type", mealtype },
               { "location", location },
               { "soup", soup },
               { "main_dish", maindish },
               { "description", description }
            };

            var request = new FormUrlEncodedContent(values);
            //URI
            HttpResponseMessage response = await client.PostAsync("http://ticketnow.ddns.net:5000/api/meal", request);

            if (response.IsSuccessStatusCode) return true;

            else return false;
        }

    }

}