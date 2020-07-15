using System;
using System.Collections.Generic;
using System.Text;

namespace Photoconnect.Classes
{
    class PortifolioRequest
    {
        [JsonProperty("event_type")]
        public string EventType { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("album_id")]
        public string AlbumId { get; set; }
    }
}
