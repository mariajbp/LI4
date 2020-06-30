﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Android.OS;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class ChangePassAdmin : ContentPage
    {
        private string token;
        private long LastButtonClickTime = 0;

        public ChangePassAdmin(string token)
        {
           
            this.token = token;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void onChangePasswordClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            bool match = matchPass(NEWPASS1.Text, NEWPASS2.Text);
            if (match == true)
            {
                if (String.IsNullOrWhiteSpace(NEWPASS1.Text))
                {
                    await DisplayAlert("", "Invalid Credentials", "Try Again");
                    return;
                }
                bool res = await this.putPassword(OLDPASS.Text, NEWPASS1.Text, token);
                if (res)
                {
                    await DisplayAlert("", "Password changed with success", "Ok");
                }
                else await DisplayAlert("", "Invalid Credentials", "Try Again");
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

        public async Task<bool> putPassword(string old_password, string new_password, string accessToken)
        {

            //POST
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            //PARAMETERS
            var values = new Dictionary<string, string>
            {
               { "id_user", USERNAME.Text },
               { "old_password", old_password },
               { "new_password", new_password }
            };

            var request = new FormUrlEncodedContent(values);
            //URI
            HttpResponseMessage response = await client.PutAsync("http://ticket-now.ddns.net:5000/api/user", request);

            if (response.IsSuccessStatusCode) return true;

            else return false;
        }

    }

}