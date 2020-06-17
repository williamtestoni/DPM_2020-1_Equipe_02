using Photoconnect.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(Photoconnect.Droid.Classes.Config))]
namespace Photoconnect.Droid.Classes
{
    public class Config : IConfig
    {
        private string diretorioDB;

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
    }
}