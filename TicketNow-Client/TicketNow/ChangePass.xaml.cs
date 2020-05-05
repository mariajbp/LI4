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

            bool match = matchPass(NEWPASS1.Text, NEWPASS2.Text);
            if (match == true)
            {

                User u = new User();
                bool res = await u.putPassword(OLDPASS.Text, NEWPASS1.Text, token);
                if (res)
                {
                    await DisplayAlert("", "Password changed with success", "Ok");
                }
                else await DisplayAlert("", "Current password is wrong", "Try Again");
            }
            else await DisplayAlert("", "Inserted passwords don't match", "Try Again");

        }


        private bool matchPass(string p1, string p2)
        {
            bool r;
            if (p1 == p2) r = true;
            else r = false;
            return r;

        }

    }

}