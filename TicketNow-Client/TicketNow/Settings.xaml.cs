using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class Settings : ContentPage
    {
        string token;

        public Settings(string token)
        {
            this.token = token;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void onChangePasswordClicked(object sender, EventArgs args)
        {

            await Navigation.PushAsync(new ChangePass(token));

        }

        private async void onLogoutClicked(object sender, EventArgs args)
        {

            await Navigation.PushAsync(new MainPage());

        }


    }

}