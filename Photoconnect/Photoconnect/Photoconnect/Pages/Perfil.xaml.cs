using Newtonsoft.Json;
using Photoconnect.Classes;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Photoconnect.Classes.Utilities;

namespace Photoconnect.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Perfil : ContentPage
    {
        private Session sessaoUsuario;

        public Perfil(Session sessaoUsuario)
        {
            InitializeComponent();

            this.Padding = new Thickness(10, 20, 10, 10);

            this.sessaoUsuario = sessaoUsuario;

            saveButton.Clicked += SaveButton_Clicked;
            voltarButton.Clicked += VoltarButton_Clicked;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameEntry.Text))
            {
                await DisplayAlert("Atençao", "Insira um Nome", "Aceitar");
                nameEntry.Focus();
                return;
            }

            this.AtualizarUsuario(this.sessaoUsuario);
        }

        private async void AtualizarUsuario(Session sessaoUsuario)
        {
            waitActivityIndicator.IsRunning = true;
            saveButton.IsEnabled = false;

            string passwordNovo;
            string passwordNovoConfirmacao;

            if (passwordNovoEntry.Text != null)
            {
                passwordNovo = passwordNovoEntry.Text;
            }
            else
            {
                passwordNovo = passwordAtualEntry.Text;
            }

            if(passwordConfirmacaoEntry.Text != null)
            {
                passwordNovoConfirmacao = passwordConfirmacaoEntry.Text;
            }
            else
            {
                passwordNovoConfirmacao = passwordAtualEntry.Text;
            }

            if(passwordNovo != passwordNovoConfirmacao)
            {
                await DisplayAlert("Atenção", "Senha incorreta", "Aceitar");
                waitActivityIndicator.IsRunning = false;
                saveButton.IsEnabled = true;

                return;
            }

            var updateRequest = new UpdateRequest
            {
                avatar_id = 2,
                name = nameEntry.Text,
                email = emailEntry.Text,
                phone_number = telefoneEntry.Text,
                street = ruaEntry.Text,
                street_number = long.Parse(numeroEntry.Text),
                complement = complementoEntry.Text,
                state = estadoEntry.Text,
                city = cidadeEntry.Text,
                neighborhood = bairroEntry.Text,
                zip_code = cepEntry.Text,
                oldPassword = passwordAtualEntry.Text,
                password = passwordNovo,
                confirmPassword = passwordNovoConfirmacao,
            };

            var jsonRequest = JsonConvert.SerializeObject(updateRequest);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var resp = string.Empty;

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://159.203.107.218");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessaoUsuario.Token);
                var url = "/users";

                var result = await client.PutAsync(url, httpContent);

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
            await Navigation.PushAsync(new dashboard(sessaoUsuario));
        }

        private async void VoltarButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new dashboard(sessaoUsuario));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this.sessaoUsuario.User.Avatar != null)
            {
                photoImage.Source = this.sessaoUsuario.User.Avatar.Url.AbsoluteUri;
                photoImage.WidthRequest = 200;
                photoImage.HeightRequest = 200;
            }

            if(this.sessaoUsuario.User.Name != null)
                nameEntry.Text = this.sessaoUsuario.User.Name;

            if (this.sessaoUsuario.User.Email != null)
                emailEntry.Text = this.sessaoUsuario.User.Email;

            if (this.sessaoUsuario.User.Street != null)
                ruaEntry.Text = this.sessaoUsuario.User.Street;

            if (this.sessaoUsuario.User.StreetNumber != null)
                numeroEntry.Text = this.sessaoUsuario.User.StreetNumber.ToString();

            if (this.sessaoUsuario.User.Complement != null)
                complementoEntry.Text = this.sessaoUsuario.User.Complement;

            if (this.sessaoUsuario.User.Neighborhood != null)
                bairroEntry.Text = this.sessaoUsuario.User.Neighborhood;

            if (this.sessaoUsuario.User.City != null)
                cidadeEntry.Text = this.sessaoUsuario.User.City;

            if (this.sessaoUsuario.User.State != null)
                estadoEntry.Text = this.sessaoUsuario.User.State;

            if (this.sessaoUsuario.User.ZipCode != null)
                cepEntry.Text = this.sessaoUsuario.User.ZipCode.ToString();

            if (this.sessaoUsuario.User.PhoneNumber != null)
                telefoneEntry.Text = this.sessaoUsuario.User.PhoneNumber;

            if (this.sessaoUsuario.password != null)
                passwordAtualEntry.Text = this.sessaoUsuario.password;
        }
    }
}