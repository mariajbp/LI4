using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TicketNow
{
    public partial class WeeklyMeal : ContentPage
    {
        public WeeklyMeal()
        {
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

        public void menuNormal()
        {
            lv.ItemsSource = new List<Meal>
            {
                 new Meal {Day="Monday - 12/04/2020", Mealss= new List<Meal1> {new Meal1 {Lun_din="Lunch", Content= "Sopa de Feijão" + "\n" + "Frango Assado" + "\n" +
                 "Arroz de Brócolos" + "\n" + "Cenourinha Baby Cozida" }, new Meal1{Lun_din="Dinner", Content="Creme de Tomate" + "\n" + "Carne de Vaca Estufada" + "\n" +
                 "Massa Cozida" +"\n"+ "Ervilhas e Feijão Verde"} }  },

                 new Meal {Day="Tuesday - 13/04/2020", Mealss= new List<Meal1> {new Meal1 {Lun_din="Lunch", Content= "Sopa de Feijão" + "\n" + "Frango Assado" + "\n" +
                 "Arroz de Brócolos" + "\n" + "Cenourinha Baby Cozida" }, new Meal1{Lun_din="Dinner", Content="Creme de Tomate" + "\n" + "Carne de Vaca Estufada" + "\n" +
                 "Massa Cozida" +"\n"+  "Ervilhas e Feijão Verde"} }  },

                 new Meal {Day="Wednesday - 14/04/2020", Mealss= new List<Meal1> {new Meal1 {Lun_din="Lunch", Content= "Sopa de Feijão" + "\n" + "Frango Assado" + "\n" +
                 "Arroz de Brócolos" + "\n" + "Cenourinha Baby Cozida" }, new Meal1{Lun_din="Dinner", Content="Creme de Tomate" + "\n" + "Carne de Vaca Estufada" + "\n" +
                 "Massa Cozida" +"\n"+  "Ervilhas e Feijão Verde"} }  },

                 new Meal {Day="Thursday - 15/04/2020", Mealss= new List<Meal1> {new Meal1 {Lun_din="Lunch", Content= "Sopa de Feijão" + "\n" + "Frango Assado" + "\n" +
                 "Arroz de Brócolos" + "\n" + "Cenourinha Baby Cozida" }, new Meal1{Lun_din="Dinner", Content="Creme de Tomate" + "\n" + "Carne de Vaca Estufada" + "\n" +
                 "Massa Cozida" +"\n"+  "Ervilhas e Feijão Verde"} }  },

                 new Meal {Day="Friday - 16/04/2020", Mealss= new List<Meal1> {new Meal1 {Lun_din="Lunch", Content= "Sopa de Feijão" + "\n" + "Frango Assado" + "\n" +
                 "Arroz de Brócolos" + "\n" + "Cenourinha Baby Cozida" }, new Meal1{Lun_din="Dinner", Content="Creme de Tomate" + "\n" + "Carne de Vaca Estufada" + "\n" +
                 "Massa Cozida" +"\n"+  "Ervilhas e Feijão Verde"} }  }
                 };

        }

        public void menuVegetarian()
        {
            lv.ItemsSource = new List<Meal>
            {
                 new Meal {Day="Monday - 12/04/2020", Mealss= new List<Meal1> {new Meal1 {Lun_din="Lunch", Content= "Sopa de Feijão" + "\n" + "Lentilhas Estufadas" + "\n" +
                 "Puré de Batata" + "\n" + "Couve Branca Salteada" }, new Meal1{Lun_din="Dinner", Content="Creme de Tomate" + "\n" + "Beringela Recheada com Soja" + "\n" +
                 "Arroz" +"\n"+ "Salada de Alface e Cenoura"} }  },

                 new Meal {Day="Tuesday - 13/04/2020", Mealss= new List<Meal1> {new Meal1 {Lun_din="Lunch", Content= "Sopa de Feijão" + "\n" + "Lentilhas Estufadas" + "\n" +
                 "Puré de Batata" + "\n" + "Couve Branca Salteada" }, new Meal1{Lun_din="Dinner", Content="Creme de Tomate" + "\n" + "Beringela Recheada com Soja" + "\n" +
                 "Arroz" +"\n"+ "Salada de Alface e Cenoura"} }  },

                 new Meal {Day="Wednesday - 14/04/2020", Mealss= new List<Meal1> {new Meal1 {Lun_din="Lunch", Content= "Sopa de Feijão" + "\n" + "Lentilhas Estufadas" + "\n" +
                 "Puré de Batata" + "\n" + "Couve Branca Salteada" }, new Meal1{Lun_din="Dinner", Content="Creme de Tomate" + "\n" + "Beringela Recheada com Soja" + "\n" +
                 "Arroz" +"\n"+ "Salada de Alface e Cenoura"} }  },

                 new Meal {Day="Thursday - 15/04/2020", Mealss= new List<Meal1> {new Meal1 {Lun_din="Lunch", Content= "Sopa de Feijão" + "\n" + "Lentilhas Estufadas" + "\n" +
                 "Puré de Batata" + "\n" + "Couve Branca Salteada" }, new Meal1{Lun_din="Dinner", Content="Creme de Tomate" + "\n" + "Beringela Recheada com Soja" + "\n" +
                 "Arroz" +"\n"+ "Salada de Alface e Cenoura"} }  },

                 new Meal {Day="Friday - 16/04/2020", Mealss= new List<Meal1> {new Meal1 {Lun_din="Lunch", Content= "Sopa de Feijão" + "\n" + "Lentilhas Estufadas" + "\n" +
                 "Puré de Batata" + "\n" + "Couve Branca Salteada" }, new Meal1{Lun_din="Dinner", Content="Creme de Tomate" + "\n" + "Beringela Recheada com Soja" + "\n" +
                 "Arroz" +"\n"+ "Salada de Alface e Cenoura"} }  }
                 };

        }


    }
    
}
