using System.Collections.Generic;
using Entry = Microcharts.Entry;
using Xamarin.Forms;
using SkiaSharp;
using Microcharts;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace TicketNow
{
    public partial class Charts : ContentPage
    {
        List<Entry> entries = new List<Entry>();
        List<Entry> entries1 = new List<Entry>();
        List<Entry> entries2 = new List<Entry>();

        public Charts(string id_user, string token)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            var x= this.setHistory(id_user, token);

        }

        public async Task<bool> setHistory(string id_user, string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //HISTORY
            //test with server
            HttpResponseMessage resp = await client.GetAsync("http://ticketnow.ddns.net:5000/api/user/" + id_user + "/history");
            HttpContent cont = resp.Content;

            //Read the string.
            string r = await cont.ReadAsStringAsync();


            JObject hist = Newtonsoft.Json.Linq.JObject.Parse(r);


            JArray a = (JArray)hist["history"];

            IList<History> history = a.ToObject<IList<History>>();

            IList<string> datas =new List<string>();

            //list of tickets (number of tickets)
            int i = 0; var color = "0";
            foreach (var t in history)
            {
                string[] data=t.used_datetime.Split(' ');
                datas.Add(data[0]);
            }

            var noDups = new HashSet<string>(datas);
            
            //Day
            foreach (var n in noDups)
            {
                i = 0;
                foreach (var d in datas)
                {
                    if (n == d) i++;
                }

                if (color == "490403") color = "ab0a07";
                else if (color == "ab0a07") color = "dc0d09";
                else if (color == "dc0d09") color = "f62623";
                else if (color == "f62623") color = "f96e6c";
                else if (color == "f96e6c") color = "0";
                else if (color == "0") color = "490403";

                this.entries.Add(new Entry(i)
                {
                    Color = SKColor.Parse(color),
                    Label = n,
                    ValueLabel = i.ToString()
                }
                   ) ;

            }


            //WEEK





            //MONTH





            chart1.Chart = new BarChart() { Entries = entries, LabelTextSize=38, Margin=30, MinValue=0 };
            chart2.Chart = new LineChart() { Entries = entries, LabelTextSize = 38, Margin=30,MinValue=0 };
            chart3.Chart = new DonutChart() { Entries = entries, LabelTextSize = 40};


            return true;
        }
    }

}
