using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class Settings : ContentPage
    {
        User u;
        public Settings(User u)
        {
            this.u = u;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void onChangePasswordClicked(object sender, EventArgs args)
        {

            var oldpass = u.changePass(OLDPASS.Text);
            if (oldpass == true)
            {
                var match = u.matchPass(NEWPASS1.Text,NEWPASS2.Text);
                if (match == true)
                {
                    // password is not changed in the server YET !!!!!
                    await DisplayAlert("", "Password changed with success", "Ok");
                }
                else await DisplayAlert("", "Inserted passwords don't match", "Try Again");
            }
            else await DisplayAlert("", "Current password is wrong", "Try Again");

       }
    }
}
