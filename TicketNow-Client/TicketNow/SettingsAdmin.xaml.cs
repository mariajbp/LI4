using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Android.OS;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class SettingsAdmin : ContentPage
    {
        private User u;
        private string token;
        private long LastButtonClickTime = 0;

        public SettingsAdmin(User u, string token)
        {
            this.u = u;
            this.token = token;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void onChangePasswordClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            await Navigation.PushAsync(new ChangePassAdmin(token));
        }

        //arranjar
        private async void onDeleteUserClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            await Navigation.PushAsync(new DeleteUser(token));
        }


        private async void onLogoutClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            await logout();
            await Navigation.PushAsync(new MainPage());

        }


        public async Task<bool> logout()
        {
            //POST
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);



            //test with server

            //PARAMETERS
            var values = new Dictionary<string, string>
            {
            };

            var request = new FormUrlEncodedContent(values);
            //URI
            HttpResponseMessage response = await client.PostAsync("http://ticketnow.ddns.net:5000/api/logout", request);
            return true;

        }

    }
}