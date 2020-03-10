using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using TicketNow;
using Android.Content;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace TicketNow
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = Xamarin.Forms.Forms.Context.GetDrawable(Resource.Drawable.RoundCorner);
                Control.SetPadding(10, 0, 0, 0);
            }
        }
    }
    }
}