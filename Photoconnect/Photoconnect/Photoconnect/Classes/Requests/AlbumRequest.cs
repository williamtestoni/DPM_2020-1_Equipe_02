using Newtonsoft.Json;

namespace Photoconnect.Classes
{
    class AlbumRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
