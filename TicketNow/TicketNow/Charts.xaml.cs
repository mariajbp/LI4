using System.Collections.Generic;
using Entry = Microcharts.Entry;
using Xamarin.Forms;
using SkiaSharp;

namespace TicketNow
{
    public partial class Charts : ContentPage
    {
        List<Entry> entries = new List<Entry>
        {
            new Entry(200)
            {
                Color = SKColor.Parse("490403"),
                Label = "January",
                ValueLabel = "200"
            },

            new Entry(300)
            {
                Color = SKColor.Parse("7a0705"),
                Label = "February",
                ValueLabel = "200"
            },
            new Entry(100)
            {
                Color = SKColor.Parse("ab0a07"),
                Label = "March",
                ValueLabel = "100"
            },
            new Entry(150)
            {
                Color = SKColor.Parse("dc0d09"),
                Label = "April",
                ValueLabel = "150"
            },
            new Entry(300)
            {
                Color = SKColor.Parse("f62623"),
                Label = "May",
                ValueLabel = "300"
            },
            new Entry(350)
            {
                Color = SKColor.Parse("f96e6c"),
                Label = "June",
                ValueLabel = "350"
            }
        };

        public Charts()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            chart1.Chart = new BarChart() { Entries = entries };
            chart2.Chart = new LineChart() { Entries = entries };
            chart3.Chart = new PointChart() { Entries = entries };
            chart4.Chart = new DonutChart() { Entries = entries };

        }
    }
}
