using System;
using System.Collections.Generic;
using Android.OS;
using Xamarin.Forms;

namespace TicketNow
    
{
    public partial class BuyTickets : ContentPage
    {
        private long LastButtonClickTime = 0;
        private string username;
        private string token;

        public BuyTickets(String username,string token)
        {
            this.username = username;
            this.token = token;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }


        private async void onCompleteMealButtonClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            await Navigation.PushAsync(new BuyTickets2(0,this.token,this.username));

        }

        private async void onSimpleMealButtonClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            await Navigation.PushAsync(new BuyTickets2(1,this.token,this.username));

        }
    }
}
