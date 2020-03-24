using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TicketNow
{
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void onChangePasswordClicked(object sender, EventArgs args)
        {
            await DisplayAlert("", "Password changed with success", "Ok");
            //await DisplayAlert("", "Current password is wrong", "Try Again");
            //await DisplayAlert("", "Inserted passwords don't match", "Try Again");
        }
    }
}
