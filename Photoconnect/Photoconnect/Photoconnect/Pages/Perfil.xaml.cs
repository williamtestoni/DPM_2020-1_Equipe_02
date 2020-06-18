using Photoconnect.Classes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

            voltarButton.Clicked += VoltarButton_Clicked;
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
                numeroEntry.Text = this.sessaoUsuario.User.StreetNumber;

            if (this.sessaoUsuario.User.Complement != null)
                complementoEntry.Text = this.sessaoUsuario.User.Complement;

            if (this.sessaoUsuario.User.Neighborhood != null)
                bairroEntry.Text = this.sessaoUsuario.User.Neighborhood;

            if (this.sessaoUsuario.User.City != null)
                cidadeEntry.Text = this.sessaoUsuario.User.City;

            if (this.sessaoUsuario.User.State != null)
                estadoEntry.Text = this.sessaoUsuario.User.State;

            if (this.sessaoUsuario.User.ZipCode != null)
                cepEntry.Text = this.sessaoUsuario.User.ZipCode;

            if (this.sessaoUsuario.User.PhoneNumber != null)
                telefoneEntry.Text = this.sessaoUsuario.User.PhoneNumber;
        }
    }
}