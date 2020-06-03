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


            MealData meal = new MealData();
            meal.date = date;
            meal.meal_type = mealtype;
            meal.location = location;
            meal.soup = soup;
            meal.main_dish = maindish;
            meal.description = description;


           var json = JsonConvert.SerializeObject(meal);

        

            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);


            var httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //URI
            HttpResponseMessage response =await client.PostAsync("http://ticketnow.ddns.net:5000/api/meal", byteContent);


            if (response.IsSuccessStatusCode) return true;

            else return false;
        }

    }

}