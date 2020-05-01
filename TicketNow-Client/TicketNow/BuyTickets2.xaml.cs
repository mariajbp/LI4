using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class BuyTickets2 : ContentPage
    {

        int meal; //0- complete meal-2.75(25);    1- simple meal-2.05
        int ticket_type;
        double s = 0;
        string token;
        string username;

        public BuyTickets2(int i,string token, string username)
        {
            this.username = username;
            this.token = token;
            this.meal = i;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            if (meal == 0) { total.Text = "TOTAL: 2.75 €"; this.s = 2.75; }
            if (meal == 1) { total.Text = "TOTAL: 2.05 €"; this.s = 2.05; }
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
            if (meal == 1)
            {
                ticket_type = 1;
                await payment(ticket_type, this.s);
                
            }


            if (meal == 0)
            {
                ticket_type = 2;
                await payment(ticket_type, this.s);
                
            }

        }

        public async Task<bool> payment(int ticket, double amount)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            //test with server
            var request = new StringContent("");
            HttpResponseMessage response = await client.PostAsync("http://ticketnow.ddns.net:5000/api/kiosk/ticket?ticket_type="+ ticket+"&amount="+amount, request);
            HttpContent content = response.Content;

            await DisplayAlert("", "Payment Successful", "Ok");


            //refresh user info with new tickets
            User u = new User();
            await u.setInfo(token, username);
            await Navigation.PushAsync(new Perfil(u, token));

            return true;
        }
    }
}
