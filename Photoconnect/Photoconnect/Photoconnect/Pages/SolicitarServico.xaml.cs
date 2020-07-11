
using Photoconnect.Classes;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
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