using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TicketNow
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void onLoginButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Perfil());

        }
    }
}

