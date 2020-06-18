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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            this.Padding = new Thickness(10, 20, 10, 10);

            saveButton.Clicked += SaveButton_Clicked;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameEntry.Text))
            {
                await DisplayAlert("Atençao", "Insira um Nome", "Aceitar");
                nameEntry.Focus();
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
                await DisplayAlert("Atençao", "Insira uma Senha", "Aceitar");
                passwordEntry.Focus();
                return;
            }

            this.RegistarUsuario();
        }

        private async void RegistarUsuario()
        {
            waitActivityIndicator.IsRunning = true;
            saveButton.IsEnabled = false;

            var registerRequest = new RegisterRequest
            {
                name = nameEntry.Text,
                email = emailEntry.Text,
                password = passwordEntry.Text,
            };

            var jsonRequest = JsonConvert.SerializeObject(registerRequest);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var resp = string.Empty;

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://159.203.107.218");
                var url = "/users";
                var result = await client.PostAsync(url, httpContent);

                if (!result.IsSuccessStatusCode)
                {
                    await DisplayAlert("Atenção", result.Content.ToString(), "Aceitar");
                    waitActivityIndicator.IsRunning = false;
                    saveButton.IsEnabled = true;

                    return;
                }

                resp = await result.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Atenção", ex.Message, "Aceitar");
                waitActivityIndicator.IsRunning = false;
                saveButton.IsEnabled = true;

                return;
            }

            waitActivityIndicator.IsRunning = false;
            saveButton.IsEnabled = true;

            var userResponse = JsonConvert.DeserializeObject<Session>(resp);
            await Navigation.PushAsync(new Login());
        }
    }
}