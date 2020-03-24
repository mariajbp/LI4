using System;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void onLoginButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Perfil());

        }

        private async void onCreateAccountButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new CreateAccount());

        }

        private async void onErrorButtonClicked(object sender, EventArgs args)
        {
            await DisplayAlert("Error","Invalid Credentials","Try Again");
        }

        private async void onAdminButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Admin());
        }

    }
}

