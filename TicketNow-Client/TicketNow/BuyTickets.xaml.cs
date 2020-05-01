using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TicketNow
    
{
    public partial class BuyTickets : ContentPage
    {
        string username;
        string token;
        public BuyTickets(String username,string token)
        {
            this.username = username;
            this.token = token;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }


        private async void onCompleteMealButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new BuyTickets2(0,this.token,this.username));

        }

        private async void onSimpleMealButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new BuyTickets2(1,this.token,this.username));

        }
    }
}
