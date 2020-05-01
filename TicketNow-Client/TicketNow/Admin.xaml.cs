using System;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class Admin : ContentPage
    {
        string token;
        public Admin(string token)
        {
            this.token = token;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void onSettingsButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Settings(token));

        }

        private async void onWeeklymealsButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new WeeklyMeal());

        }


        private async void onStatsButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Charts());

        }
    }
}
