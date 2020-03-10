using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content;
using TicketNow;

[assembly: ExportRenderer(typeof(Entry), typeof(EntryRendCustom))]
namespace TicketNow
{
    public class EntryRendCustom: EntryRenderer
    {
        public EntryRendCustom(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.LightGreen);
            }
        }
    }
}
}