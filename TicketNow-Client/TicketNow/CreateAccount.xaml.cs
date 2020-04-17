using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TicketNow
{
    public partial class CreateAccount : ContentPage
    {
        public CreateAccount()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void onCreateAccountButtonClicked(object sender, EventArgs args)
        {
            await DisplayAlert("", "Your account has been created, please check your institutional email", "Ok");
            //await DisplayAlert("Erro", "Invalid Email", "Try Again");
        }
    }
}
