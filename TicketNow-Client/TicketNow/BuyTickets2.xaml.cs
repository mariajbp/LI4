using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Android.OS;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace TicketNow
{
    public partial class BuyTickets2 : ContentPage
    {

        private int meal; //0- complete meal-2.75(25);    1- simple meal-2.05
        private int ticket_type;
        private double s = 0;
        private string token;
        private string username;
        private long LastButtonClickTime = 0;

        public BuyTickets2(int i, string token, string username)
        {
            this.username = username;
            this.token = token;
            this.meal = i;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            if (meal == 0) { total.Text = "TOTAL: 2.75 €"; }
            if (meal == 1) { total.Text = "TOTAL: 2.05 €"; }
        }

        private void onMinusButtonClicked(object sender, EventArgs args)
        {
            int tmp = 1;
            if (quantity.Text == "1") ;
            else
            {
                tmp = Convert.ToInt32(quantity.Text);
                tmp--;
                quantity.Text = tmp.ToString();
            }
            this.s = 0;
            if (meal == 0) this.s = tmp * 2.75;
            if (meal == 1) this.s = tmp * 2.05;
            total.Text = "TOTAL: " + this.s.ToString("0.00") + " €";


        }

        private void onPlusButtonClicked(object sender, EventArgs args)
        {
            int tmp;
            if (quantity.Text == "10") tmp = 10;
            else
            {
                tmp = Convert.ToInt32(quantity.Text);
                tmp++;
                quantity.Text = tmp.ToString();
            }
            this.s = 0;
            if (meal == 0 && tmp == 10) this.s = 25.00;
            if (meal == 0 && tmp != 10) this.s = tmp * 2.75;
            if (meal == 1) this.s = tmp * 2.05;
            total.Text = "TOTAL: " + this.s.ToString("0.00") + " €";

        }

        private async void onPaymentButtonClicked(object sender, EventArgs args)
        {
            pay.IsEnabled = false;
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            if (meal == 1)
            {
                ticket_type = 1;
                await payment(ticket_type.ToString(), quantity.Text);

            }


            if (meal == 0)
            {
                ticket_type = 2;
                await payment(ticket_type.ToString(), quantity.Text);

            }

        }

        public async Task<bool> payment(string ticket, string amount)
        {
            //POST
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);



            //test with server

            //PARAMETERS
            var values = new Dictionary<string, string>
            {
               { "ticket_type", ticket },
               { "amount", amount }
            };

            var request = new FormUrlEncodedContent(values);
            //URI
            HttpResponseMessage response = await client.PostAsync("http://ticket-now.ddns.net:5000/api/kiosk/ticket", request);

            string r = await response.Content.ReadAsStringAsync();


            JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(r);

            string res = jObject.Value<string>("transaction_status");
            
            if (res == "Payment")
            {

                string s = jObject.Value<string>("paypal_link");

                string id = jObject.Value<string>("id_transaction");

               
                await recursive(client,id,s);

            }

      
            //refresh user info with new tickets
            User u = new User();
            await u.setInfo(token, username);
            if (u.permissoes != 1) await Navigation.PushAsync(new Admin(u, token));
            else if (u.permissoes == 1) await Navigation.PushAsync(new Perfil(u, token));
            pay.IsEnabled = true;

            return true;
        }

public async Task<bool> recursive(HttpClient client, string id,string s)
        {
            await Browser.OpenAsync(s);

            await Task.Delay(5800);

            bool action = await DisplayAlert("", "Finish payment?", "Yes", "No");

            if (action)
            {
                var method = new HttpMethod("PATCH");

                var requestt = new HttpRequestMessage(method, "http://ticket-now.ddns.net:5000/api/kiosk/ticket?id_transaction=" + id);

                var responsee = await client.SendAsync(requestt);

                string ll = await responsee.Content.ReadAsStringAsync();

                JObject jObjectt = Newtonsoft.Json.Linq.JObject.Parse(ll);

                string w = jObjectt.Value<string>("transaction_status");

                if (w == "Success"){
                    await DisplayAlert("", "Payment Successfully Completed", "Ok");
                    return true;
                }

                if (w == "Complete") await DisplayAlert("", "Payment Expired", "Ok");

                if (w == "Payment")
                {
                    await DisplayAlert("", "Payment not completed, please access your paypal account", "Ok");
                    await recursive(client,id,s);
                }
                return true;
            } 
            else return false;
        }

    }
}