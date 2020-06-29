using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Android.OS;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class DeleteUser : ContentPage
    {
        private string token;
        private long LastButtonClickTime = 0;

        public DeleteUser(string token)
        {

            this.token = token;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void onDeleteUserClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
           

                bool res = await this.deleteUser(USERNAME.Text, token);
                if (res)
                {
                    await DisplayAlert("", "User deleted with success", "Ok");
                }
                //FALTA ARRANJAR SE O ID FOR INVALIDO
                else await DisplayAlert("", "Invalid ID", "Try Again");
           
        }


        public async Task<bool> deleteUser(string id_user, string accessToken)
        {

            //DELETE
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


            //URI
            HttpResponseMessage response = await client.DeleteAsync("http://ticket-now.ddns.net:5000/api/user?id_user="+id_user);

            if (response.IsSuccessStatusCode) return true;

            else return false;
        }

    }

}