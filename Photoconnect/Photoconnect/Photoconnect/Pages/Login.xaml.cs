using Newtonsoft.Json;
using Photoconnect.Classes;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Photoconnect.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();

            this.Padding = new Thickness(10, 20, 10, 10);

            loginButton.Clicked += LoginButton_Clicked;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            photoLogo.Source = "http://159.203.107.218:3333/files/ddee8977f892a5d6b9e172de52d2d25d.PNG";
            photoLogo.WidthRequest = 260;
            photoLogo.HeightRequest = 260;
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(emailEntry.Text))
            {
                await DisplayAlert("Atenção", "Digite um e-mail", "OK");
                emailEntry.Focus();

                return;
            }

            if (!Utilities.ValidarEmail(emailEntry.Text))
            {
                await DisplayAlert("Atenção", "Digite um e-mail valido!", "OK");
                emailEntry.Focus();

                return;
            }

            if (string.IsNullOrEmpty(passwordEntry.Text))
            {
                await DisplayAlert("Atenção", "Digite uma senha", "OK");
                passwordEntry.Focus();

                return;
            }

            this.Logar();
        }

        private async void Logar()
        {
            waitActivityIndicator.IsRunning = true;

            var loginRequest = new LoginRequest
            {
                email = emailEntry.Text,
                password = passwordEntry.Text,
            };

            var jsonRequest = JsonConvert.SerializeObject(loginRequest);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var resp = string.Empty;

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://159.203.107.218");
                var url = "/sessions";
                var result = await client.PostAsync(url, httpContent);

                if(!result.IsSuccessStatusCode)
                {
                    await DisplayAlert("Atenção", "Usuário ou senha incorreto", "Aceitar");
                    waitActivityIndicator.IsRunning = false;
                    return;
                }

                resp = await result.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Atenção", ex.Message, "Aceitar");
                waitActivityIndicator.IsRunning = false;
                return;
            }

            var sessionUser = JsonConvert.DeserializeObject<Session>(resp);
            sessionUser.password = passwordEntry.Text;
            waitActivityIndicator.IsRunning = false;
            await Navigation.PushAsync(new dashboard(sessionUser));
        }
    }
}