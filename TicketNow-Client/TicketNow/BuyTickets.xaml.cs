using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TicketNow
{
    public partial class BuyTickets : ContentPage
    {
        public BuyTickets()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }


        private async void onCompleteMealButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new BuyTickets2(0));

        }

        private async void onSimpleMealButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new BuyTickets2(1));

        }
    }
}
