using System;


namespace Gateway.Model.KeyCloak.ViewModel
{
    public class UserKeyCloakVM
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public bool enabled { get; set; }
    }
}
