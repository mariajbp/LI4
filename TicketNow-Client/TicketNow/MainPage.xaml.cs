using System;
using System.Threading.Tasks;
using Android.OS;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class MainPage : ContentPage
    {
        private long LastButtonClickTime = 0;

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void onLoginButtonClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            RestClient<Task> _restClient = new RestClient<Task>();

            //get token with username and password: done in the RestClient class
            var token = await _restClient.checkLogin(EntryUsername.Text, EntryPassword.Text);
            if (token != null)
            {

                //login with token and id_user
                User u = new User();
                await u.setInfo(token, EntryUsername.Text);

                if (u.permissoes != 1) await Navigation.PushAsync(new Admin(u,token));
                else if (u.permissoes == 1) await Navigation.PushAsync(new Perfil(u,token));
            }
            else
            {
                await DisplayAlert("Error", "Invalid Credentials", "Try Again");
            }


        }

        private async void onCreateAccountButtonClicked(object sender, EventArgs args)
        {
            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000) return;
            LastButtonClickTime = SystemClock.ElapsedRealtime();
            await Navigation.PushAsync(new CreateAccount());

        }

       


    }
}




