using Gateway.Model.KeyCloak.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.KeyCloak.IServices
{
    public interface IAuthService
    {
        public Task<LoginKeycloakDto> Login(string username, string password);
        public Task<LoginKeycloakDto> RefreshToken(string token);
    }
}
