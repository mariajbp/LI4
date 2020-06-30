using System.Collections.Generic;
using Entry = Microcharts.Entry;
using Xamarin.Forms;
using SkiaSharp;
using Microcharts;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System;

namespace TicketNow
{
    public partial class Charts : ContentPage
    {
        private string token;
        

        public Charts(string token)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            this.token = token;
            var s=  globa();

        }

      

        private async Task<bool> globa() {
            List<Entry> entries = new List<Entry>();
            List<Entry> entries1 = new List<Entry>();
            List<Entry> entries2 = new List<Entry>();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Global
            DateTime dtb = begin.Date.AddDays(-1);
            DateTime dte = end.Date.AddDays(1);
            string beginn = dtb.Year + "-" + dtb.Month.ToString().PadLeft(2, '0') + "-" + dtb.Day.ToString().PadLeft(2, '0');
            string endd = dte.Year + "-" + dte.Month.ToString().PadLeft(2, '0') + "-" + dte.Day.ToString().PadLeft(2, '0');
            HttpResponseMessage resp = await client.GetAsync("http://ticket-now.ddns.net:5000/api/statistics/global?begin=" + beginn + "&end=" + endd);
            HttpContent cont = resp.Content;

            //Read the string.
            string r = await cont.ReadAsStringAsync();


            JObject hist = Newtonsoft.Json.Linq.JObject.Parse(r);


            JObject s = hist.Value<JObject>("statistics");


            IList<History> history = new List<History>();
            var data = s.Children();
            string datee; string lunchh = ""; string dinneer = "";
            foreach (var q in data)
            {

                JProperty jProperty = q.ToObject<JProperty>();
                datee = jProperty.Name;

                var values = jProperty.Values();
                foreach (var v in values)
                {

                    JProperty jjProperty = v.ToObject<JProperty>();
                    string ff = jjProperty.Name;
                    if (ff == "lunch")
                    {
                        var g = jjProperty.Values();
                        foreach (var hj in g)
                            lunchh = hj.ToString();
                    }
                    if (ff == "dinner")
                    {
                        var gg = jjProperty.Values();
                        foreach (var hjg in gg)
                            dinneer = hjg.ToString();
                    }

                }
                History h = new History(datee, lunchh, dinneer);
                history.Add(h);
                int dat = s.Count;
            }
            {
                if (history == null) Console.WriteLine("sad");
                int sum_lunch = 0; int sum_din = 0; string color = "490403";
                foreach (var d in history)
                {
                    if (color == "490403") color = "ab0a07";
                    else if (color == "ab0a07") color = "dc0d09";
                    else if (color == "dc0d09") color = "f62623";
                    else if (color == "f62623") color = "f96e6c";
                    else if (color == "f96e6c") color = "0";
                    else if (color == "0") color = "490403";

                    sum_lunch = sum_lunch + Int32.Parse(d.lunch);
                    sum_din = sum_din + Int32.Parse(d.dinner);
                    entries.Add(new Entry(Convert.ToUInt32(d.lunch))
                    {
                        Color = SKColor.Parse(color),
                        Label = d.date,
                        ValueLabel = d.lunch
                    }
                           );

                    entries1.Add(new Entry(Convert.ToUInt32(d.dinner))
                    {
                        Color = SKColor.Parse(color),
                        Label = d.date,
                        ValueLabel = d.dinner
                    }
                       );


                }
                entries2.Add(new Entry(sum_lunch)
                {
                    Color = SKColor.Parse("490403"),
                    Label = "Lunch",
                    ValueLabel = sum_lunch.ToString()
                }
                   );

                entries2.Add(new Entry(sum_din)
                {
                    Color = SKColor.Parse("dc0d09"),
                    Label = "Dinner",
                    ValueLabel = sum_din.ToString()
                }
                   );
                
                if (s.Count == 0)
                {
                    lun.Text = "There's nothing here, yet... Please select another date range!";
                    lun.IsVisible = true;
                    chart1.Chart = new BarChart() { Entries = entries, BackgroundColor = SKColors.Transparent, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart4.Chart = new BarChart() { Entries = entries1, BackgroundColor = SKColors.Transparent, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart3.Chart = new DonutChart() { Entries = entries2, BackgroundColor = SKColors.Transparent, LabelTextSize = 40 };
                }
                else
                {
                    glo.IsVisible = true;
                    din.IsVisible = true;
                    lun.Text="Lunch";
                    chart1.Chart = new BarChart() { Entries = entries, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart4.Chart = new BarChart() { Entries = entries1, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart3.Chart = new DonutChart() { Entries = entries2, LabelTextSize = 40 };
                }

                

            }
            return true;
        }

        private async void onGlobalButtonClicked(object sender, EventArgs args)
        {
            var s=await globa();
        }

        ///////////////////////////ANOTHER//////////////////////////////

        private async void onMeButtonClicked(object sender, EventArgs args)
        {
            List<Entry> entries = new List<Entry>();
            List<Entry> entries1 = new List<Entry>();
            List<Entry> entries2 = new List<Entry>();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Me
            DateTime dtb = begin.Date.AddDays(-1);
            DateTime dte = end.Date.AddDays(1);
            string beginn = dtb.Year + "-" + dtb.Month.ToString().PadLeft(2, '0') + "-" + dtb.Day.ToString().PadLeft(2, '0');
            string endd = dte.Year + "-" + dte.Month.ToString().PadLeft(2, '0') + "-" + dte.Day.ToString().PadLeft(2, '0');
            HttpResponseMessage resp = await client.GetAsync("http://ticket-now.ddns.net:5000/api/statistics/me?begin=" + beginn + "&end=" + endd);
            HttpContent cont = resp.Content;

            //Read the string.
            string r = await cont.ReadAsStringAsync();


            JObject hist = Newtonsoft.Json.Linq.JObject.Parse(r);


            JObject s = hist.Value<JObject>("statistics");


            IList<History> history = new List<History>();
            var data = s.Children();
            string datee; string lunchh = ""; string dinneer = "";
            foreach (var q in data)
            {

                JProperty jProperty = q.ToObject<JProperty>();
                datee = jProperty.Name;

                var values = jProperty.Values();
                foreach (var v in values)
                {

                    JProperty jjProperty = v.ToObject<JProperty>();
                    string ff = jjProperty.Name;
                    if (ff == "lunch")
                    {
                        var g = jjProperty.Values();
                        foreach (var hj in g)
                            lunchh = hj.ToString();
                    }
                    if (ff == "dinner")
                    {
                        var gg = jjProperty.Values();
                        foreach (var hjg in gg)
                            dinneer = hjg.ToString();
                    }

                }
                History h = new History(datee, lunchh, dinneer);
                history.Add(h);
                int dat = s.Count;
                
            }
            {  
                 
                if (history == null) Console.WriteLine("sad");
                int sum_lunch = 0; int sum_din = 0; string color = "490403";
                foreach (var d in history)
                {
                    if (color == "490403") color = "ab0a07";
                    else if (color == "ab0a07") color = "dc0d09";
                    else if (color == "dc0d09") color = "f62623";
                    else if (color == "f62623") color = "f96e6c";
                    else if (color == "f96e6c") color = "0";
                    else if (color == "0") color = "490403";

                    sum_lunch = sum_lunch + Int32.Parse(d.lunch);
                    sum_din = sum_din + Int32.Parse(d.dinner);
                    entries.Add(new Entry(Convert.ToUInt32(d.lunch))
                    {
                        Color = SKColor.Parse(color),
                        Label = d.date,
                        ValueLabel = d.lunch
                    }
                           );

                    entries1.Add(new Entry(Convert.ToUInt32(d.dinner))
                    {
                        Color = SKColor.Parse(color),
                        Label = d.date,
                        ValueLabel = d.dinner
                    }
                       );


                }
                entries2.Add(new Entry(sum_lunch)
                {
                    Color = SKColor.Parse("490403"),
                    Label = "Lunch",
                    ValueLabel = sum_lunch.ToString()
                }
                   );

                entries2.Add(new Entry(sum_din)
                {
                    Color = SKColor.Parse("dc0d09"),
                    Label = "Dinner",
                    ValueLabel = sum_din.ToString()
                }
                   );
                if (s.Count ==0)
                {
                    lun.Text = "There's nothing here, yet... Please select another date range!";
                    lun.IsVisible = true;
                    chart1.Chart = new BarChart() { Entries = entries, BackgroundColor = SKColors.Transparent, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart4.Chart = new BarChart() { Entries = entries1, BackgroundColor = SKColors.Transparent, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart3.Chart = new DonutChart() { Entries = entries2, BackgroundColor = SKColors.Transparent, LabelTextSize = 40 };
                }
                else
                {
                    glo.IsVisible = true;
                    din.IsVisible = true;
                    lun.Text = "Lunch";
                    chart1.Chart = new BarChart() { Entries = entries, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart4.Chart = new BarChart() { Entries = entries1, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart3.Chart = new DonutChart() { Entries = entries2, LabelTextSize = 40 };
                }



               
            }
        }
    }
        }
    
