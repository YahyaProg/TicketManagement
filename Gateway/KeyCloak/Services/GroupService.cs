using Core.GenericResultModel;
using Gateway.KeyCloak.IServices;
using Gateway.KeyCloak.Utils;
using Gateway.Model.KeyCloak.Dto;
using Infrastructure.Repositories.Setting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Gateway.KeyCloak.Services
{
    public class GroupService(IApiClient _api, IHttpContextAccessor _httpContextAccessor, IDbSettings _dbSetting) : ApiClientHelper(_httpContextAccessor, _dbSetting), IGroupService
    {
        private static string _ApiService => "/admin/realms/master/groups";

        public async Task<ApiResult<List<RoleKeycloakDto>>> GetAllAsync()
        {
            var url = ServiceAddress(_ApiService);

            var headers = SetAuthorizationHeader();

            var response = await _api.GetAsync(url, headers);

            if (response.StatusCode is not HttpStatusCode.OK)
                return FaildReturn<List<RoleKeycloakDto>>(response.StatusCode);

            var result = await ConvertJsonToData<List<RoleKeycloakDto>>(response);

            return SuccessReturn(result);
        }

        public async Task<ApiResult<RoleKeycloakDto>> GetAsync(Guid id)
        {
            var url = ServiceAddress($"{_ApiService}/{id}");

            var headers = SetAuthorizationHeader();

            var response = await _api.GetAsync(url, headers);

            if (response.StatusCode is not HttpStatusCode.OK)
                return FaildReturn<RoleKeycloakDto>(response.StatusCode);

            var result = await ConvertJsonToData<RoleKeycloakDto>(response);

            return SuccessReturn(result);
        }
    }
}
