using Core.GenericResultModel;
using Gateway.KeyCloak.IServices;
using Gateway.KeyCloak.Utils;
using Gateway.Model.KeyCloak.Role;
using Gateway.Model.KeyCloak.RoleGroup;
using Infrastructure.Repositories.Setting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.KeyCloak.Services
{
    public class RoleGroupService(IApiClient _api, IHttpContextAccessor _httpContextAccessor, IDbSettings _dbSetting) : ApiClientHelper(_httpContextAccessor, _dbSetting), IRoleGroupService
    {
        private static string _ApiService(Guid id)
        {
            var apiService = string.Concat("/admin/realms/master/groups/", id, "/role-mappings/realm");
            return apiService;
        }

        public async Task<ApiResult<List<RoleVM>>> GetAsync(Guid id)
        {
            var url = ServiceAddress(_ApiService(id));

            var headers = SetAuthorizationHeader();

            var response = await _api.GetAsync(url, headers);

            if (response.StatusCode is not HttpStatusCode.OK)
                return FaildReturn<List<RoleVM>>(response.StatusCode);

            var result = await ConvertJsonToData<List<RoleVM>>(response);

            return SuccessReturn(result);
        }

        public async Task<ApiResult<bool>> AddAsync(List<AddRoleGroupParams> parameters, Guid groupId)
        {
            var url = ServiceAddress(_ApiService(groupId));

            var headers = SetAuthorizationHeader();

            var response = await _api.PostAsync(url, parameters, headers);

            if (response is not { StatusCode: HttpStatusCode.OK or HttpStatusCode.NoContent })
            {
                if (response is { StatusCode: HttpStatusCode.Conflict })
                    return FaildReturn<bool>(response.StatusCode, "رول از قبل موجود است", "Role exists");

                return FaildReturn<bool>(response.StatusCode);
            }
            return SuccessReturn(true);
        }

        private async Task<HttpResponseMessage> DeleteResponseAsync(List<DeleteRoleGroupParams> parameters, Guid groupId)
        {
            var url = ServiceAddress(_ApiService(groupId));

            var headers = SetAuthorizationHeader();

            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);
            request.Method = HttpMethod.Delete;

            request.Headers.Add("Authorization", headers.FirstOrDefault().Value);

            var bodyString = JsonConvert.SerializeObject(parameters);
            var content = new StringContent(bodyString, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await client.SendAsync(request);
            return response;
        }

        public async Task<ApiResult<bool>> DeleteAsync(List<DeleteRoleGroupParams> parameters, Guid groupId)
        {

            var response = await DeleteResponseAsync(parameters, groupId);

            if (response is not { StatusCode: HttpStatusCode.OK or HttpStatusCode.NoContent })
            {
                return FaildReturn<bool>(response.StatusCode);
            }
            return SuccessReturn(true);
        }
    }
}