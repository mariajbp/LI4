using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;


namespace TicketNow
{
    public partial class Perfil : ContentPage

    {
        User u;
        string token;
        Ticket complete;
        Ticket simple;

        public Perfil(User u, string token)
        {
           
            this.u = u;
            this.token = token;

            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            IList<Ticket> tickets = u.owned_tickets;

            if (u.cm == 0);
            else
            {
                foreach (var t in tickets)
                {
                    if (t.type == 2) this.complete = t;
                    break;
                }
            }

            if (u.sm == 0);
            else
            {
                foreach (var t in tickets)
                {
                    if (t.type == 1) this.simple = t;
                    break;
                }
            }


            if (u.cm == 1)
            {
                meals.Text = "YOU HAVE " + u.cm.ToString() + " COMPLETE MEAL AVAILABLE";
               // barcod.BarcodeValue = complete.id_ticket;
            }
            else if (u.cm != 0)
            {
                meals.Text = "YOU HAVE " + u.cm.ToString() + " COMPLETE MEALS AVAILABLE";
               // barcod.BarcodeValue = complete.id_ticket;
            }
            else
            {
                barcod.Opacity = 0.5;
                meals.Text = "YOU HAVE 0 COMPLETE MEALS AVAILABLE";
            }


        }


        protected override bool OnBackButtonPressed()
        {
            return true;
        }



        private async void onSettingsButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Settings(token));

        }

        private async void onWeeklymealsButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new WeeklyMeal());

        }


        private async void onBuyticketsButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new BuyTickets(u.id_user, token));

        }

        private async void onStatsButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Charts());

        }



        private void onRightButtonClicked(object sender, EventArgs args)
        {



            right.Opacity = 0.5;
            left.Opacity = 1;
            if (u.sm == 1)
            {
                meals.Text = "YOU HAVE " + u.sm.ToString() + " SIMPLE MEAL AVAILABLE"; barcod.Opacity = 1;
              //  barcod.BarcodeValue = simple.id_ticket;
            }
            else if (u.sm != 0)
            {
                meals.Text = "YOU HAVE " + u.sm.ToString() + " SIMPLE MEALS AVAILABLE"; barcod.Opacity = 1;
                Console.WriteLine("AAAAAAAAAAAL" + simple.id_user);
               // barcod.BarcodeValue = simple.id_ticket;
            }
            else
            {
                barcod.Opacity = 0.5;
                meals.Text = "YOU HAVE 0 SIMPLE MEALS AVAILABLE";
            }
        }

        private void onLeftButtonClicked(object sender, EventArgs args)
        {



            right.Opacity = 1;
            left.Opacity = 0.5;
            if (u.cm == 1)
            {
                meals.Text = "YOU HAVE " + u.cm.ToString() + " COMPLETE MEAL AVAILABLE"; barcod.Opacity = 1;
             //   barcod.BarcodeValue = complete.id_ticket;
            }
            else if (u.cm != 0)
            {
                meals.Text = "YOU HAVE " + u.cm.ToString() + " COMPLETE MEALS AVAILABLE"; barcod.Opacity = 1;
              //  barcod.BarcodeValue = complete.id_ticket;
            }
            else
            {
                barcod.Opacity = 0.5;
                meals.Text = "YOU HAVE 0 COMPLETE MEALS AVAILABLE";
            }
        }

       
    }

}