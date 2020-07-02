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
    public partial class WeeklyMeal : ContentPage
    {
        public IList<MealData>  mealss { get; set; }
        private long LastButtonClickTime = 0;

        public WeeklyMeal(string token)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            var s=this.setMeals(token);
          
        }

        public async Task<bool> setMeals(string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            HttpResponseMessage resp = await client.GetAsync("http://ticket-now.ddns.net:5000/api/meal");

        //Read the string.
        string r = await resp.Content.ReadAsStringAsync();

            JObject meal = Newtonsoft.Json.Linq.JObject.Parse(r);


            JArray a = (JArray)meal["meals"];

            IList<MealData> m = a.ToObject<IList<MealData>>();

            this.mealss = m;

            menuNormal();
            return true;

        }


private void onLocationButtonClicked( object sender, EventArgs args)
        {
            string k =local.Text;
            if (k == "Gualtar")
            {

                local.Text = "Azurém";
                linhadir.Opacity = 0.5;
                linhadir1.Opacity = 0.5;
                linhaesq.Opacity = 1;
                linhaesq1.Opacity = 1;
                utensesq.Opacity = 1;
                utensdir.Opacity = 0.5;
                normalmeal.Opacity = 1;
                veg.Opacity = 0.5;
                menuNormal();
            }
            else
            {
                local.Text = "Gualtar";
                linhadir.Opacity = 0.5;
                linhadir1.Opacity = 0.5;
                linhaesq.Opacity = 1;
                linhaesq1.Opacity = 1;
                utensesq.Opacity = 1;
                utensdir.Opacity = 0.5;
                normalmeal.Opacity = 1;
                veg.Opacity = 0.5;
                menuNormal();
            }
           
        }


private void onVegetarianButtonClicked(object sender, EventArgs args)
        { 
            linhadir.Opacity = 1;
            linhadir1.Opacity = 1;
            linhaesq.Opacity = 0.5;
            linhaesq1.Opacity = 0.5;
            utensesq.Opacity = 0.5;
            utensdir.Opacity = 1;
            normalmeal.Opacity = 0.5;
            veg.Opacity = 1;
            menuVegetarian();
        }

        private void onMealsButtonClicked(object sender, EventArgs args)
        {
            linhadir.Opacity = 0.5;
            linhadir1.Opacity = 0.5;
            linhaesq.Opacity = 1;
            linhaesq1.Opacity = 1;
            utensesq.Opacity = 1;
            utensdir.Opacity = 0.5;
            normalmeal.Opacity = 1;
            veg.Opacity = 0.5;
            menuNormal();
        }

        public void menuNormal()
        {
            IList<MealData> s = new List<MealData>();
            int g;
            if (local.Text == "Gualtar") g=1;
            else g = 2;
            if (this.mealss == null)
            {
                lv.ItemsSource = s;
                return;
            }
            foreach (var me in this.mealss)
            {
                if (me.meal_type == "lunch" || me.meal_type == "dinner")
                {
                    if ((me.location == "gualtar" && g==1) || (me.location=="azurem" && g==2)) { 
                s.Add(new MealData {date = me.date, location= char.ToUpper(me.location[0])+me.location.Substring(1),
                meal_type= char.ToUpper(me.meal_type[0]) + me.meal_type.Substring(1), soup=me.soup, main_dish=me.main_dish,description=me.description});
                }
                    }
            }

            lv.ItemsSource = s;

        }

        public void menuVegetarian()
        {
            IList<MealData> s = new List<MealData>();
            int g;
            if (local.Text == "Gualtar") g = 1;
            else g = 2;
            if (this.mealss == null)
            {
                lv.ItemsSource = s;
                return;
            }
            foreach (var me in this.mealss)
            {
                if (me.meal_type == "dinner_veg" ){
                if ((me.location == "gualtar" && g == 1) || (me.location == "azurem" && g == 2))
                    {
                        s.Add(new MealData
                        {
                            
                            date = me.date,
                            location = char.ToUpper(me.location[0]) + me.location.Substring(1),
                            meal_type = "Dinner",
                            soup = me.soup,
                            main_dish = me.main_dish,
                            description = me.description
                        });
                    } }

                if ( me.meal_type == "lunch_veg")
                {
                    if ((me.location == "gualtar" && g == 1) || (me.location == "azurem" && g == 2))
                    {
                        s.Add(new MealData
                        {

                            date = me.date,
                            location = char.ToUpper(me.location[0]) + me.location.Substring(1),
                            meal_type = "Lunch",
                            soup = me.soup,
                            main_dish = me.main_dish,
                            description = me.description
                        });
                    }
                }

            }

            lv.ItemsSource = s;

        }







    }

}
