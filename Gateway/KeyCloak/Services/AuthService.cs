using Gateway.KeyCloak.IServices;
using Gateway.Model.KeyCloak.Dto;
using Infrastructure.Repositories.Setting;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Gateway.KeyCloak.Services
{
    public class AuthService(IDbSettings _dbSettings) : IAuthService
    {
        private static string _AuthService => "/realms/master/protocol/openid-connect/token";
        public async Task<LoginKeycloakDto> Login(string username, string password)
        {
            HttpClient client = new HttpClient();

            var basePath = _dbSettings.GetSetting("keycloak_connection_url");
            var loginPath = _AuthService;
            var url = basePath + loginPath;
            var client_id = _dbSettings.GetSetting("keycloak_resource");
            var client_secret = _dbSettings.GetSetting("keycloak_credentials_secret");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{url}");
            request.Content = new StringContent($"grant_type=password&client_id={client_id}&username={username}&password={password}&client_secret={client_secret}");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode is not HttpStatusCode.OK)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginKeycloakDto>(responseBody);

            return result;

        }

        public async Task<LoginKeycloakDto> RefreshToken(string token)
        {
            HttpClient client = new HttpClient();

            var basePath = _dbSettings.GetSetting("keycloak_connection_url");
            var loginPath = _AuthService;
            var url = basePath + loginPath;
            var client_id = _dbSettings.GetSetting("keycloak_resource");
            var client_secret = _dbSettings.GetSetting("keycloak_credentials_secret");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{url}");
            request.Content = new StringContent($"grant_type=refresh_token&client_id={client_id}&client_secret={client_secret}&refresh_token={token}");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode is not HttpStatusCode.OK)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginKeycloakDto>(responseBody);

            return result;
        }
    }
}
