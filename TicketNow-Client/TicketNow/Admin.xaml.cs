using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.OS;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace TicketNow
{
    public partial class Admin : ContentPage

    {
        private User u;
        private string token;
        private Ticket complete;
        private Ticket simple;
        private int leftt = 1; //0=right; 1=left;
        private long LastButtonClickTime = 0;

        public Admin(User u, string token)
        {

            this.u = u;
            this.token = token;

            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();


            IList<Ticket> tickets = u.owned_tickets;

            if (u.cm == 0) ;
            else
            {
                foreach (var t in tickets)
                {
                    if (t.type == 2) this.complete = t;

                }
            }

            if (u.sm == 0) ;
            else
            {
                foreach (var t in tickets)
                {
                    if (t.type == 1) this.simple = t;

                }
            }


            if (u.cm == 1)
            {
                meals.Text = "YOU HAVE " + u.cm.ToString() + " COMPLETE MEAL AVAILABLE";
                barcod.BarcodeValue = complete.id_ticket + " " + complete.id_user;
            }
            else if (u.cm != 0)
            {
                meals.Text = "YOU HAVE " + u.cm.ToString() + " COMPLETE MEALS AVAILABLE";
                barcod.BarcodeValue = complete.id_ticket + " " + complete.id_user;
            }
            else
            {
                barcod.Opacity = 0.5;
                meals.Text = "YOU HAVE 0 COMPLETE MEALS AVAILABLE";
            }


        }

        private async void refresh()
        {
            await u.setInfo(token, u.id_user);
            IList<Ticket> tickets = u.owned_tickets;

            if (u.cm == 0) ;
            else
            {
                foreach (var t in tickets)
                {
                    if (t.type == 2) this.complete = t;

                }
            }

            if (u.sm == 0) ;
            else
            {
                foreach (var t in tickets)
                {
                    if (t.type == 1) this.simple = t;

                }
            }

            if (leftt == 0)
            {
                right.Opacity = 0.5;
                left.Opacity = 1;
                if (u.sm == 1)
                {
                    meals.Text = "YOU HAVE " + u.sm.ToString() + " SIMPLE MEAL AVAILABLE"; barcod.Opacity = 1;
                    barcod.BarcodeValue = simple.id_ticket + " " + simple.id_user;
                }
                else if (u.sm != 0)
                {
                    meals.Text = "YOU HAVE " + u.sm.ToString() + " SIMPLE MEALS AVAILABLE"; barcod.Opacity = 1;
                    barcod.BarcodeValue = simple.id_ticket + " " + simple.id_user;
                }
                else
                {
                    barcod.Opacity = 0.5;
                    meals.Text = "YOU HAVE 0 SIMPLE MEALS AVAILABLE";
                }
            }
            else if (leftt == 1)
            {
                right.Opacity = 1;
                left.Opacity = 0.5;
                if (u.cm == 1)
                {
                    meals.Text = "YOU HAVE " + u.cm.ToString() + " COMPLETE MEAL AVAILABLE"; barcod.Opacity = 1;
                    barcod.BarcodeValue = complete.id_ticket + " " + complete.id_user;
                }
                else if (u.cm != 0)
                {
                    meals.Text = "YOU HAVE " + u.cm.ToString() + " COMPLETE MEALS AVAILABLE"; barcod.Opacity = 1;
                    barcod.BarcodeValue = complete.id_ticket + " " + complete.id_user;
                }
                else
                {
                    barcod.Opacity = 0.5;
                    meals.Text = "YOU HAVE 0 COMPLETE MEALS AVAILABLE";
                }
            }

        }


        private async void onRefreshButtonClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            this.refresh();
            

        }


        protected override bool OnBackButtonPressed()
        {

            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            this.refresh();
            return true;
        }



        private async void onSettingsButtonClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            //refresh user info with new ticke
            this.refresh();
            await Navigation.PushAsync(new Settings(u,token));
            

        }

        private async void onWeeklymealsButtonClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            //refresh user info with new ticket
            this.refresh();
            await Navigation.PushAsync(new WeeklyMeal(token));
            
        }


        private async void onBuyticketsButtonClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            //refresh user info with new ticke
            this.refresh();
            await Navigation.PushAsync(new BuyTickets(u.id_user, token));
            
        }

        private async void onStatsButtonClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            //refresh user info with new ticke
            this.refresh();
            await Navigation.PushAsync(new Charts());
           
        }



        private async void onRightButtonClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            this.leftt = 0;
            //refresh user info with new ticke
            await u.setInfo(token, u.id_user);
            IList<Ticket> tickets = u.owned_tickets;

            if (u.cm == 0) ;
            else
            {
                foreach (var t in tickets)
                {
                    if (t.type == 2) this.complete = t;

                }
            }

            if (u.sm == 0) ;
            else
            {
                foreach (var t in tickets)
                {
                    if (t.type == 1) this.simple = t;

                }
            }
            right.Opacity = 0.5;
            left.Opacity = 1;
            if (u.sm == 1)
            {
                meals.Text = "YOU HAVE " + u.sm.ToString() + " SIMPLE MEAL AVAILABLE"; barcod.Opacity = 1;
                barcod.BarcodeValue = simple.id_ticket + " " + simple.id_user;
            }
            else if (u.sm != 0)
            {
                meals.Text = "YOU HAVE " + u.sm.ToString() + " SIMPLE MEALS AVAILABLE"; barcod.Opacity = 1;
                Console.WriteLine("AAAAAAAAAAAL" + simple.id_user);
                barcod.BarcodeValue = simple.id_ticket + " " + simple.id_user;
            }
            else
            {
                barcod.Opacity = 0.5;
                meals.Text = "YOU HAVE 0 SIMPLE MEALS AVAILABLE";
            }
            
        }

        private async void onLeftButtonClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            this.leftt = 1;
            //refresh user info with new ticke
            await u.setInfo(token, u.id_user);
            IList<Ticket> tickets = u.owned_tickets;

            if (u.cm == 0) ;
            else
            {
                foreach (var t in tickets)
                {
                    if (t.type == 2) this.complete = t;

                }
            }

            if (u.sm == 0) ;
            else
            {
                foreach (var t in tickets)
                {
                    if (t.type == 1) this.simple = t;

                }
            }



            right.Opacity = 1;
            left.Opacity = 0.5;
            if (u.cm == 1)
            {
                meals.Text = "YOU HAVE " + u.cm.ToString() + " COMPLETE MEAL AVAILABLE"; barcod.Opacity = 1;
                barcod.BarcodeValue = complete.id_ticket + " " + complete.id_user;
            }
            else if (u.cm != 0)
            {
                meals.Text = "YOU HAVE " + u.cm.ToString() + " COMPLETE MEALS AVAILABLE"; barcod.Opacity = 1;
                barcod.BarcodeValue = complete.id_ticket + " " + complete.id_user;
            }
            else
            {
                barcod.Opacity = 0.5;
                meals.Text = "YOU HAVE 0 COMPLETE MEALS AVAILABLE";
            }
            

        }

        //codigo para a app do validator


        private void onValidatorButtonClicked(object sender, EventArgs e)

        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            Scanner();
            
        }


        public async void Scanner()
        {

            var ScannerPage = new ZXingScannerPage();

            ScannerPage.OnScanResult += (result) =>
            {
                // stop scanning
                ScannerPage.IsScanning = false;

                // Alert with scanning code
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    await DisplayAlert("", "Done", "OK");
                    await val(result.Text);

                });
            };


             await Navigation.PushAsync(ScannerPage);

        }

        public async Task<bool> val(string result)
        {
            //POST
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string[] words = result.Split(' ');

            //test with server

            //PARAMETERS
            var values = new Dictionary<string, string>
            {
               { "id_user", words[1] },
               { "id_ticket", words[0] }
            };

            var request = new FormUrlEncodedContent(values);
            //URI
            HttpResponseMessage response = await client.PostAsync("http://ticketnow.ddns.net:5000/api/validator", request);



            return true;


        }
    }
}