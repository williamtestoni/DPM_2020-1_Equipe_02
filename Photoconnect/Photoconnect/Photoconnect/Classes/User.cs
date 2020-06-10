using System;
using System.Collections.Generic;
using System.Text;

namespace Photoconnect.Classes
{
    public class User
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Street { get; set; }

        public long StreetNumber { get; set; }

        public string Complement { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Neighborhood { get; set; }

        public long ZipCode { get; set; }

        public bool Provider { get; set; }
        
        public string Token { get; set; }
    }
}
