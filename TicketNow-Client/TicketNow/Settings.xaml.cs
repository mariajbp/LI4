using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class Settings : ContentPage
    {
        User u;
        string token;
        public Settings(User u,string token)
        {
            this.u = u;
            this.token = token;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
        
        private async void onChangePasswordClicked(object sender, EventArgs args)
        {
            if(u.permissoes==1)   
            await Navigation.PushAsync(new ChangePass(u,token));
            else if(u.permissoes==3)
                await Navigation.PushAsync(new ChangePassAdmin(token));
        }

        private async void onLogoutClicked(object sender, EventArgs args)
        {
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