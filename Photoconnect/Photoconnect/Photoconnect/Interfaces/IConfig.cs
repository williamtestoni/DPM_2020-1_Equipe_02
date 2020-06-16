using SQLite.Net.Interop;

namespace Photoconnect.Interfaces
{
    public interface IConfig
    {
        string DiretorioDB { get; }
        
        ISQLitePlatform Plataforma { get; }
    }
}
