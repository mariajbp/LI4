using System;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class Admin : ContentPage
    {
        public Admin()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void onSettingsButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Settings());

        }

        private async void onWeeklymealsButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new WeeklyMeal());

        }


        private async void onStatsButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Statistics());

        }
    }
}
