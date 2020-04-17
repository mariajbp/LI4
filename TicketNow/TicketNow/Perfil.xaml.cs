using System;

using Xamarin.Forms;


namespace TicketNow
{
    public partial class Perfil : ContentPage

    {
        User u;
        public Perfil(string token, string id_user)
        {
            //user authentication with token and id_user
            u = new User(token, id_user);


            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            if (u.cm != 0) meals.Text = "YOU HAVE " + u.cm.ToString() + " COMPLETE MEALS AVAILABLE";
            else
            {
                barcod.Opacity = 0.5;
                meals.Text = "YOU HAVE 0 COMPLETE MEALS AVAILABLE";
            }


        }


        private async void onSettingsButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Settings());

        }

        private async void onWeeklymealsButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new WeeklyMeal());

        }


        private async void onBuyticketsButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new BuyTickets());

        }

        private async void onStatsButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Charts());

        }



        private void onRightButtonClicked(object sender, EventArgs args)
        {
            right.Opacity = 0.5;
            left.Opacity = 1;
            if (u.sm != 0) { meals.Text = "YOU HAVE " + u.sm.ToString() + " SIMPLE MEALS AVAILABLE"; barcod.Opacity = 1; }
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
            if (u.cm != 0) { meals.Text = "YOU HAVE " + u.cm.ToString() + " COMPLETE MEALS AVAILABLE"; barcod.Opacity = 1; }
            else
            {
                barcod.Opacity = 0.5;
                meals.Text = "YOU HAVE 0 COMPLETE MEALS AVAILABLE";
            }
        }


    }

}
