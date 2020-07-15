using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Photoconnect.Classes
{
    class SolicitacaoRequest
    {
        [JsonProperty("event_type")]
        public string EventType { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("street_number")]
        public long StreetNumber { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("neighborhood")]
        public string Neighborhood { get; set; }

        [JsonProperty("start_event")]
        public DateTimeOffset StartEvent { get; set; }

        [JsonProperty("end_event")]
        public DateTimeOffset EndEvent { get; set; }

        [JsonProperty("provider_id")]
        public long ProviderId { get; set; }
    }
}
