using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;
using ZXing.QrCode;

namespace TicketNow
{
    public partial class Perfil : ContentPage

    {
        int cm = 3; //complete meal
        int sm = 0; //simple meal

        public Perfil()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            if (cm != 0) meals.Text = "YOU HAVE " + cm.ToString() + " COMPLETE MEALS AVAILABLE";
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
            if (sm != 0) { meals.Text = "YOU HAVE " + sm.ToString() + " SIMPLE MEALS AVAILABLE"; barcod.Opacity = 1; }
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
            if (cm != 0) { meals.Text = "YOU HAVE " + cm.ToString() + " COMPLETE MEALS AVAILABLE"; barcod.Opacity = 1; }
            else
            {
                barcod.Opacity = 0.5;
                meals.Text = "YOU HAVE 0 COMPLETE MEALS AVAILABLE";
            }
            }


    }
    
}

