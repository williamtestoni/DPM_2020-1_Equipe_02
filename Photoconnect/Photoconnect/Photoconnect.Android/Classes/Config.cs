using Photoconnect.Interfaces;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly : Dependency(typeof(Photoconnect.Droid.Classes.Config))]
namespace Photoconnect.Droid.Classes
{
    public class Config : IConfig
    {
        private string diretorioDB;
        private ISQLitePlatform plataforma;

        public string DiretorioDB 
        {
            get 
            {
                if(string.IsNullOrEmpty(diretorioDB))
                {
                    diretorioDB = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }

                return diretorioDB;
            }
            
        }

        public ISQLitePlatform Plataforma
        {
            get
            {
                if (plataforma == null)
                {
                    plataforma = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                    
                }
                
                return plataforma;
            }

        } 
    }
}