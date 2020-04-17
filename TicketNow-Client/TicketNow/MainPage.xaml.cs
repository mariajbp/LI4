using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TicketNow
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void onLoginButtonClicked(object sender, EventArgs args)
        {

            RestClient<Task> _restClient = new RestClient<Task>();

            //get token with username and password: done in the RestClient class
            var token = await _restClient.checkLogin(EntryUsername.Text, EntryPassword.Text);

            if (token != null)
            {
                //login with token and id_user
                await Navigation.PushAsync(new Perfil(token, EntryUsername.Text));
            }
            else
            {
                await DisplayAlert("Error", "Invalid Credentials", "Try Again");
            }


        }

        private async void onCreateAccountButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new CreateAccount());

        }

        private async void onAdminButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Admin());
        }


    }
}




