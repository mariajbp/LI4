using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TicketNow
{
    public partial class BuyTickets2 : ContentPage
    {

        int meal; //0- complete meal-2.75(25);    1- simple meal-2.05

        public BuyTickets2(int i)
        {
            meal = i;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            if (meal == 0) total.Text = "TOTAL: 2.75 €";
            if (meal == 1) total.Text = "TOTAL: 2.05 €";
        }

        private void onMinusButtonClicked(object sender, EventArgs args)
            {
            int tmp = 1;
            if (quantity.Text == "1");
            else
            {
                tmp = Convert.ToInt32(quantity.Text);
                tmp--;
                quantity.Text = tmp.ToString();
            }
                double s=0;
                if (meal == 0) s = tmp * 2.75;
                if (meal == 1) s = tmp * 2.05;
                total.Text = "TOTAL: " + s.ToString("0.00") + " €";
            

        }

        private void onPlusButtonClicked(object sender, EventArgs args)
        {
            int tmp; 
            if (quantity.Text == "10") tmp=10;
            else
            {
                tmp = Convert.ToInt32(quantity.Text);
                tmp++;
                quantity.Text = tmp.ToString();
            }
            double s = 0;
            if (meal == 0 && tmp == 10) s = 25.00;
            if (meal == 0 && tmp !=10) s = tmp * 2.75;
            if (meal == 1) s = tmp * 2.05;
            total.Text = "TOTAL: " + s.ToString("0.00") + " €";

        }
    }
}
