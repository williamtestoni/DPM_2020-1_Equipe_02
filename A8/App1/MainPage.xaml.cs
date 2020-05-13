using Plugin.ExternalMaps;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        double latitude = 0;
        double longitude = 0;
        

        private async void btGeolocalizacao_Clicked(object sender, EventArgs e)
        {
            lbGeolocalizacao.Text = "Obtendo a Geolocalização...\n";

            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                

                var position = await locator.GetPositionAsync();

                lbGeolocalizacao.Text += "Latitude: " + position.Latitude + "\n";
                lbGeolocalizacao.Text += "Longitude: " + position.Longitude + "\n";

                latitude = position.Latitude;
                longitude = position.Longitude;
         
            }
            catch(Exception ex)
            {
                await DisplayAlert("Erro : ", ex.Message, "OK");
            }
        }

        private void btMostrarPosicaoNoMapa_Clicked(object sender, EventArgs e)
        {
            try
            {
                CrossExternalMaps.Current.NavigateTo("Teste", latitude, longitude);
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro : ", ex.Message, "OK");
            }
        }

        private void btMostrarPosicaoNoMapa2_Clicked(object sender, EventArgs e)
        {
            try
            {
                Launcher.OpenAsync("geo:"+ latitude+ ","+ longitude+ "?q=");
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro : ", ex.Message, "OK");
            }
        }

        /*private void btMostrarPosicaoNoMapaSimulado_Clicked(object sender, EventArgs e)
        {
            latitude = double.Parse(LatEntry.Text);
            longitude = double.Parse(LongEntry.Text);

        }*/

       /* private async void btCoordenadasAtuais_Clicked(object sender, EventArgs e)
        {
            lbGeolocalizacao.Text = "Latitude: " + latitude + "\n";
            lbGeolocalizacao.Text += "Longitude: " + longitude + "\n";
        }

        private void btMostrarPosicaoBarcelona_Clicked(object sender, EventArgs e)
        {
            latitude = 41.387366;
            longitude = 2.170702;
        }

        private void btMostrarPosicaoTokyo_Clicked(object sender, EventArgs e)
        {
            latitude = 35.678030;
            longitude = 139.774871;
        }*/
    }
}
