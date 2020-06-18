using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Photoconnect.Classes
{
    public partial class Session
    {
        
        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        public string password { get; set; }
    }

    public partial class User
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("avatar")]
        public Avatar Avatar { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("street_number")]
        public long? StreetNumber { get; set; }

        [JsonProperty("complement")]
        public string Complement { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("neighborhood")]
        public string Neighborhood { get; set; }

        [JsonProperty("zip_code")]
        public long? ZipCode { get; set; }

        [JsonProperty("provider")]
        public bool Provider { get; set; }

        public override int GetHashCode()
        {
            return (int)this.Id;
        }
    }

    public partial class Avatar
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    
}
