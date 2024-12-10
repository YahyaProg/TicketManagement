namespace Gateway.Model.KeyCloak.Dto
{
    public class LoginKeycloakDto
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }

        // Expire Time Per Seconds
        public long refresh_expires_in { get; set; }
    }
}
