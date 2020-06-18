namespace Photoconnect.Classes
{
    class UpdateRequest
    {
        public string name { get; set; }
        public int avatar_id { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string street { get; set; }
        public long street_number { get; set; }
        public string complement { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string neighborhood { get; set; }
        public long zip_code { get; set; }
        public string oldPassword { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
    }
}
