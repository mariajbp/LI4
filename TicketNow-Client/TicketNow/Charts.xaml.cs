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
using System.Linq;

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
            List<ChartEntry> entries = new List<ChartEntry>();
            List<ChartEntry> entries1 = new List<ChartEntry>();
            List<ChartEntry> entries2 = new List<ChartEntry>();
            chart1.HeightRequest = 500;
            chart4.HeightRequest = 500;

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
            var data = s.Children(); Console.Write(data.ToString());
            string dateee; string lunchh = ""; string dinneer = "";
            foreach (var q in data)
            {

                JProperty jProperty = q.ToObject<JProperty>();
                dateee = jProperty.Name;
                DateTime datee = DateTime.ParseExact(dateee, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);

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
            IEnumerable<History> sortedEnum = history.OrderBy(f => f.date);
            IList<History> hhg = sortedEnum.ToList();

            int sum_lunch = 0; int sum_din = 0; string color = "490403";
                foreach (var d in hhg)
                {
                    
                    if (color == "490403") color = "ab0a07";
                    else if (color == "ab0a07") color = "dc0d09";
                    else if (color == "dc0d09") color = "f62623";
                    else if (color == "f62623") color = "f96e6c";
                    else if (color == "f96e6c") color = "490403";
                    

                    sum_lunch = sum_lunch + Int32.Parse(d.lunch);
                    sum_din = sum_din + Int32.Parse(d.dinner);
                    entries.Add(new ChartEntry(Convert.ToUInt32(d.lunch))
                    {
                        Color = SKColor.Parse(color),
                        Label = d.date.Year + "-" + d.date.Month.ToString().PadLeft(2, '0') + "-" + d.date.Day.ToString().PadLeft(2, '0'),
                        ValueLabel = d.lunch
                    }
                           );

                    entries1.Add(new ChartEntry(Convert.ToUInt32(d.dinner))
                    {
                        Color = SKColor.Parse(color),
                        Label = d.date.Year + "-" + d.date.Month.ToString().PadLeft(2, '0') + "-" + d.date.Day.ToString().PadLeft(2, '0'),
                        ValueLabel = d.dinner
                    }
                       );

                }

                
                entries2.Add(new ChartEntry(sum_lunch)
                {
                    Color = SKColor.Parse("490403"),
                    Label = "Lunch",
                    ValueLabel = sum_lunch.ToString()
                }
                   );

                entries2.Add(new ChartEntry(sum_din)
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
                    chart3.Chart =null;
                }
            else if (sum_din == 0 && s.Count <= 4)
            {

                glo.IsVisible = true;
                din.IsVisible = false;
                lun.IsVisible = true;
                chart1.Chart = new BarChart() { Entries = entries, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Horizontal, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                chart4.HeightRequest = 0;
                chart3.Chart = new DonutChart() { Entries = entries2, LabelTextSize = 35 };
            }
            else if (sum_din == 0 && s.Count > 4)
            {

                glo.IsVisible = true;
                din.IsVisible = false;
                lun.IsVisible = true;
                chart1.Chart = new BarChart() { Entries = entries, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Vertical, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                chart4.HeightRequest = 0;
                chart3.Chart = new DonutChart() { Entries = entries2, LabelTextSize = 35 };
            }


            else if (sum_lunch == 0 && s.Count <= 4)
            {

                glo.IsVisible = true;
                din.IsVisible = true;
                lun.IsVisible = false;
                chart4.Chart = new BarChart() { Entries = entries, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Horizontal, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                chart1.HeightRequest = 0;
                chart3.Chart = new DonutChart() { Entries = entries2, LabelTextSize = 35 };
            }
            else if (sum_lunch == 0 && s.Count > 4)
            {

                glo.IsVisible = true;
                din.IsVisible = true;
                lun.IsVisible = false;
                chart4.Chart = new BarChart() { Entries = entries, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Vertical, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                chart1.HeightRequest = 0;
                chart3.Chart = new DonutChart() { Entries = entries2, LabelTextSize = 35 };
            }

            else if (s.Count <= 4)
            {
                glo.IsVisible = true;
                din.IsVisible = true;
                lun.Text = "Lunch";
                chart1.Chart = new BarChart() { Entries = entries, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Horizontal, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                chart4.Chart = new BarChart() { Entries = entries1, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Horizontal, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                chart3.Chart = new DonutChart() { Entries = entries2, LabelTextSize = 35 };
            }
            else
                {
                    glo.IsVisible = true;
                    din.IsVisible = true;
                    lun.Text="Lunch";
                    chart1.Chart = new BarChart() { Entries = entries, ValueLabelOrientation=Orientation.Horizontal, LabelOrientation=Orientation.Vertical, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart4.Chart = new BarChart() { Entries = entries1, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Vertical, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart3.Chart = new DonutChart() { Entries = entries2, LabelTextSize = 35 };
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
            List<ChartEntry> entries = new List<ChartEntry>();
            List<ChartEntry> entries1 = new List<ChartEntry>();
            List<ChartEntry> entries2 = new List<ChartEntry>();
            chart1.HeightRequest = 500;
            chart4.HeightRequest = 500;

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
            string dateee; string lunchh = ""; string dinneer = "";
            foreach (var q in data)
            {

                JProperty jProperty = q.ToObject<JProperty>();
                dateee = jProperty.Name;
                DateTime datee = DateTime.ParseExact(dateee, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);

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
            IEnumerable<History> sortedEnum = history.OrderBy(f => f.date);
            IList<History> hhg = sortedEnum.ToList();
            {  
                 
                if (history == null) Console.WriteLine("sad");
                int sum_lunch = 0; int sum_din = 0; string color = "490403";
                foreach (var d in hhg)
                {
                    if (color == "490403") color = "ab0a07";
                    else if (color == "ab0a07") color = "dc0d09";
                    else if (color == "dc0d09") color = "f62623";
                    else if (color == "f62623") color = "f96e6c";
                    else if (color == "f96e6c") color = "490403";
           
                    sum_lunch = sum_lunch + Int32.Parse(d.lunch);
                    sum_din = sum_din + Int32.Parse(d.dinner);

                    if (d.lunch != "0")
                    {
                        entries.Add(new ChartEntry(Convert.ToUInt32(d.lunch))
                        {
                            Color = SKColor.Parse(color),
                            Label = d.date.Year + "-" + d.date.Month.ToString().PadLeft(2, '0') + "-" + d.date.Day.ToString().PadLeft(2, '0'),

                            ValueLabel = d.lunch
                        }
                               );
                    }
                    if (d.dinner != "0")
                    {
                        entries1.Add(new ChartEntry(Convert.ToUInt32(d.dinner))
                        {
                            Color = SKColor.Parse(color),
                            Label = d.date.Year + "-" + d.date.Month.ToString().PadLeft(2, '0') + "-" + d.date.Day.ToString().PadLeft(2, '0'),
                            ValueLabel = d.dinner
                        }
                       );
                    }


                }
                entries2.Add(new ChartEntry(sum_lunch)
                {
                    Color = SKColor.Parse("490403"),
                    Label = "Lunch",
                    ValueLabel = sum_lunch.ToString()
                }
                   );

                entries2.Add(new ChartEntry(sum_din)
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
                    chart4.Chart = new BarChart() { Entries = entries1, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart3.Chart = null;
                }
                else if (sum_din == 0 && s.Count<=4)
                {
                    
                    glo.IsVisible = true;
                    din.IsVisible = false;
                    lun.IsVisible = true;
                    chart1.Chart = new BarChart() { Entries = entries, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Horizontal, LabelTextSize = 38, Margin = 30, MinValue = 0};
                    chart4.HeightRequest = 0;
                    chart3.Chart = new DonutChart() { Entries = entries2, LabelTextSize = 35 };
                }
                else if (sum_din == 0 && s.Count > 4)
                {

                    glo.IsVisible = true;
                    din.IsVisible = false;
                    lun.IsVisible = true;
                    chart1.Chart = new BarChart() { Entries = entries, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Vertical, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart4.HeightRequest = 0;
                    chart3.Chart = new DonutChart() { Entries = entries2, LabelTextSize = 35 };
                }


                else if (sum_lunch == 0 && s.Count <= 4)
                {

                    glo.IsVisible = true;
                    din.IsVisible = true;
                    lun.IsVisible = false;
                    chart4.Chart = new BarChart() { Entries = entries, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Horizontal, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart1.HeightRequest = 0;
                    chart3.Chart = new DonutChart() { Entries = entries2, LabelTextSize = 35 };
                }
                else if (sum_lunch == 0 && s.Count > 4)
                {

                    glo.IsVisible = true;
                    din.IsVisible = true;
                    lun.IsVisible = false;
                    chart4.Chart = new BarChart() { Entries = entries, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Vertical, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart1.HeightRequest = 0;
                    chart3.Chart = new DonutChart() { Entries = entries2, LabelTextSize = 35 };
                }


                else if (s.Count <= 4)
                {
                    glo.IsVisible = true;
                    din.IsVisible = true;
                    lun.Text = "Lunch";
                    chart1.Chart = new BarChart() { Entries = entries, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Horizontal, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart4.Chart = new BarChart() { Entries = entries1, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Horizontal, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart3.Chart = new DonutChart() { Entries = entries2, LabelTextSize = 35 };
                }
                else
                {
                    glo.IsVisible = true;
                    din.IsVisible = true;
                    lun.Text = "Lunch";
                    chart1.Chart = new BarChart() { Entries = entries, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Vertical, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart4.Chart = new BarChart() { Entries = entries1, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Vertical, LabelTextSize = 38, Margin = 30, MinValue = 0 };
                    chart3.Chart = new DonutChart() { Entries = entries2, LabelTextSize = 35 };
                }




            }
        }
    }
        }
    
