
using Newtonsoft.Json;
using Photoconnect.Classes;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Photoconnect.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SolicitarServico : ContentPage
    {
        private Session sessaoUsuario;

        public SolicitarServico(Session sessaoUsuario)
        {
            InitializeComponent();

            this.sessaoUsuario = sessaoUsuario;
            if (sessaoUsuario.User.Id != 0)
                saveButton.Clicked += SaveButton_Clicked;

            voltarButton.Clicked += VoltarButton_Clicked;
        }

        private async void VoltarButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new dashboard(sessaoUsuario));
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            waitActivityIndicator.IsRunning = true;

            var solicitacaoRequest = new SolicitacaoRequest
            {
                EventType = (string)pcCategoria.SelectedItem,
                Description = descriptionEditor.Text,
                Street = streetEntry.Text,
                StreetNumber = long.Parse(street_numberEntry.Text),
                State = stateEntry.Text,
                City = cityEntry.Text,
                Neighborhood = bairroEntry.Text,
                StartEvent = dataInicial.Date,
                EndEvent = dataFinal.Date,
            };

            var jsonRequest = JsonConvert.SerializeObject(solicitacaoRequest);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var resp = string.Empty;

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://159.203.107.218");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessaoUsuario.Token);
                var url = "/services";

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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                await DisplayAlert("Ops", "Nenhuma câmera detectada.", "OK");

                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "Demo"
                });

            if (file == null)
                return;

            imgFoto.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;

            });
        }

        private async void btnSelecionarImagem_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Ops", "Galeria de fotos não suportada.", "OK");

                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;

            imgFoto.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;

            });
        }
    }
}