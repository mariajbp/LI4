using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Android.OS;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class DeleteMeal : ContentPage
    {
        private User u;
        private string token;
        private long LastButtonClickTime = 0;
        public DeleteMeal(User u, string token)
        {
            this.u = u;
            this.token = token;

            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void onDeleteMealClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();

                bool res = await this.deleteMeal(date.Text, mealtype.Text, location.Text, token);
                if (res)
                {
                    await DisplayAlert("", "Success", "Ok");
                }

        }



        public async Task<bool> deleteMeal(string date, string mealtype,string location, string accessToken)
        {

            //POST
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            //PARAMETERS
            var values = new Dictionary<string, string>
            {
               { "date", date },
               { "meal_type", mealtype },
               { "location", location }
            };

            var request = new FormUrlEncodedContent(values);
            //URI
            HttpResponseMessage response = await client.PostAsync("http://ticketnow.ddns.net:5000/api/meal", request);

            if (response.IsSuccessStatusCode) return true;

            else return false;
        }

    }

}