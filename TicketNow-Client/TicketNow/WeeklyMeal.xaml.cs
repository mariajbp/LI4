using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class WeeklyMeal : ContentPage
    {
        string token;
        public IList<Meal> meals { get; set; }

        public WeeklyMeal(string token)
        {
            this.token = token;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            menuNormal();
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

        public async void menuNormal()
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync("http://ticketnow.ddns.net:5000/api/meal");

            HttpContent content = response.Content;

            //Read the string.
            string result = await content.ReadAsStringAsync();


            JObject meals = Newtonsoft.Json.Linq.JObject.Parse(result);


            JArray a = (JArray)meals["meals"];

            IList<Meal> meal = a.ToObject<IList<Meal>>();

            this.meals = meal;

            List<Meal> l = new List<Meal>();
            for(int i=0; i < meal.Count - 1; i++)
            {
                if (meal[i].date == meal[i + 1].date)
                {
                    l.Add(new Meal
                    {
                        date = meal[i].date + "\n",
                        location = meal[i].location.ToUpper(new CultureInfo("en-US", false)) + "\n",
                        Mealss = new List<Meal1> { new Meal1 { meal_type = meal[i].meal_type.ToUpper(new CultureInfo("en-US", false)), Content = meal[i].soup + "\n" + meal[i].main_dish + "\n" + meal[i].description },
                    new Meal1 {meal_type = meal[i+1].meal_type.ToUpper(new CultureInfo("en-US", false)), Content = meal[i+1].soup + "\n" + meal[i+1].main_dish + "\n" + meal[i+1].description } }
                    });
                    i++;
                }
            }

           
            lv.ItemsSource = l;

        }
                
            public void menuVegetarian()
            {
                lv.ItemsSource = new List<Meal>
                {
                     new Meal {date="2020-05-05",location="GUALTAR", Mealss= new List<Meal1> {new Meal1 {meal_type="LUNCH", Content= "Sopa de Feijão" + "\n" + "Lentilhas Estufadas" + "\n" +
                     "Puré de Batata" + "\n" + "Couve Branca Salteada" }, new Meal1{meal_type="DINNER", Content="Creme de Tomate" + "\n" + "Beringela Recheada com Soja" + "\n" +
                     "Arroz" +"\n"+ "Salada de Alface e Cenoura"} }  },

                     new Meal {date="2020-05-06",location="GUALTAR", Mealss= new List<Meal1> {new Meal1 {meal_type="LUNCH", Content= "Sopa de Feijão" + "\n" + "Lentilhas Estufadas" + "\n" +
                     "Puré de Batata" + "\n" + "Couve Branca Salteada" }, new Meal1{meal_type="DINNER", Content="Creme de Tomate" + "\n" + "Beringela Recheada com Soja" + "\n" +
                     "Arroz" +"\n"+ "Salada de Alface e Cenoura"} }  },

                     new Meal {date="2020-05-07",location="GUALTAR", Mealss= new List<Meal1> {new Meal1 {meal_type="LUNCH", Content= "Sopa de Feijão" + "\n" + "Lentilhas Estufadas" + "\n" +
                     "Puré de Batata" + "\n" + "Couve Branca Salteada" }, new Meal1{meal_type="DINNER", Content="Creme de Tomate" + "\n" + "Beringela Recheada com Soja" + "\n" +
                     "Arroz" +"\n"+ "Salada de Alface e Cenoura"} }  },

                     new Meal {date="2020-05-08",location="GUALTAR", Mealss= new List<Meal1> {new Meal1 {meal_type="LUNCH", Content= "Sopa de Feijão" + "\n" + "Lentilhas Estufadas" + "\n" +
                     "Puré de Batata" + "\n" + "Couve Branca Salteada" }, new Meal1{meal_type="DINNER", Content="Creme de Tomate" + "\n" + "Beringela Recheada com Soja" + "\n" +
                     "Arroz" +"\n"+ "Salada de Alface e Cenoura"} }  },

                     new Meal {date="2020-05-09",location="GUALTAR", Mealss= new List<Meal1> {new Meal1 {meal_type="LUNCH", Content= "Sopa de Feijão" + "\n" + "Lentilhas Estufadas" + "\n" +
                     "Puré de Batata" + "\n" + "Couve Branca Salteada" }, new Meal1{meal_type="DINNER", Content="Creme de Tomate" + "\n" + "Beringela Recheada com Soja" + "\n" +
                     "Arroz" +"\n"+ "Salada de Alface e Cenoura"} }  }
                     };
                     
            }


        }


    }
