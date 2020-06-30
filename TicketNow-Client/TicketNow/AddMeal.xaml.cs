using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.OS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

            string datee = date.Date.Year + "-" + date.Date.Month.ToString().PadLeft(2, '0') + "-" + date.Date.Day.ToString().PadLeft(2, '0');

            bool res = await this.addMeal(datee, mealtype.Text, location.Text,soup.Text,maindish.Text,description.Text, token);
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
            HttpResponseMessage response =await client.PostAsync("http://ticket-now.ddns.net:5000/api/meal", byteContent);

            string r = await response.Content.ReadAsStringAsync();


            JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(r);

            string s = jObject.Value<string>("error");


            

            if (response.IsSuccessStatusCode) return true;


            else if (s.Equals("Meal type does not exists"))
            {
                await DisplayAlert("", "Please choose a meal type between Lunch, Dinner, Veg_dinner, Veg_lunch", "Ok");
            }
            else if (s.Equals("Location does not exists"))
            {
                await DisplayAlert("", "Please choose a location between Gualtar and Azurém", "Ok");
            }
            else if (s.Equals("Invalid date format (ISO Date format required)!"))
            {
                await DisplayAlert("", "Please insert a correct data format (YYYY-MM-DD)", "Ok");
            }
            else if (s.Equals("Meal already exists"))
            {
                await DisplayAlert("", "This meal already exists", "Ok");
            }

            return false;
        }

    }

}