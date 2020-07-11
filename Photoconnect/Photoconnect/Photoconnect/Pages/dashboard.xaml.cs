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
    public partial class dashboard : ContentPage
    {
        private Session sessaoUsuario;

        public dashboard(Session sessaoUsuario)
        {
            InitializeComponent();

            this.Padding = new Thickness(10, 20, 10, 10);

            this.sessaoUsuario = sessaoUsuario;

            perfilButton.Clicked += PerfilButton_Clicked;
            agendarServicoButton.Clicked += AgendarServicoButton_Clicked;
            logoutButton.Clicked += LogoutButton_ClickedAsync;
        }

        private async void AgendarServicoButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SolicitarServico(sessaoUsuario));
        }

        private async void PerfilButton_Clicked(object sender, System.EventArgs e)
        {
            var loginRequest = new LoginRequest
            {
                email = this.sessaoUsuario.User.Email,
                password = this.sessaoUsuario.password,
            };

            waitActivityIndicator.IsRunning = true;

            var jsonRequest = JsonConvert.SerializeObject(loginRequest);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var resp = string.Empty;

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://159.203.107.218");
                var url = "/sessions";
                var result = await client.PostAsync(url, httpContent);

                if (!result.IsSuccessStatusCode)
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
            sessionUser.password = sessaoUsuario.password;

            waitActivityIndicator.IsRunning = false;

            await Navigation.PushAsync(new Perfil(sessionUser));
        }

        private async void LogoutButton_ClickedAsync(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            userNameLabel.Text = this.sessaoUsuario.User.Name;

            if(this.sessaoUsuario.User.Avatar != null)
            {
                photoImage.Source = this.sessaoUsuario.User.Avatar.Url.AbsoluteUri;
                photoImage.WidthRequest = 200;
                photoImage.HeightRequest = 200;
            }

            if(this.sessaoUsuario.User.Provider)
            {
                meusAgendamentosButton.IsVisible = false;
                agendarServicoButton.IsVisible = false;
            }
            else
            {
                meusServicosButton.IsVisible = false;
            }
        }
    }
}