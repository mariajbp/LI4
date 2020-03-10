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

       
    }
}

