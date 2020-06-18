using Photoconnect.Classes;
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

            logoutButton.Clicked += LogoutButton_ClickedAsync;

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