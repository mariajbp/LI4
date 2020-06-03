using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.OS;
using Newtonsoft.Json;
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

            //Delete
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


            var request = new HttpRequestMessage(HttpMethod.Delete, "http://ticketnow.ddns.net:5000/api/meal");


            MealData meal = new MealData();
            meal.date = date;
            meal.meal_type = mealtype;
            meal.location = location;

            var json = JsonConvert.SerializeObject(meal);

            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);


            request.Content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

   

            //URI
            HttpResponseMessage response = await client.SendAsync(request);


            if (response.IsSuccessStatusCode) return true;

            else return false;
        }

    }

}