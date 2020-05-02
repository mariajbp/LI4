using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class ChangePass : ContentPage
    {
        string token;
        public ChangePass(string token)
        {
            this.token = token;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void onChangePasswordClicked(object sender, EventArgs args)
        {

            var match = matchPass(NEWPASS1.Text, NEWPASS2.Text);
            if (match == true)
            {

                var oldpass = true; // change to the http request where it devolves the true or false 
                if (oldpass == true)
                {
                    // password is not changed in the server YET !!!!!
                    await DisplayAlert("", "Password changed with success", "Ok");
                }
                else await DisplayAlert("", "Current password is wrong", "Try Again");

            }
            else await DisplayAlert("", "Inserted passwords don't match", "Try Again");

        }


        private bool matchPass(string newpass, string newpass2)
        {
            bool r;
            if (newpass == newpass2) r = true;
            else r = false;
            return r;

        }

    }

}