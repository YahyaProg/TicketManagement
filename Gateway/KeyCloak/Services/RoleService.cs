using Core.GenericResultModel;
using Gateway.KeyCloak.IServices;
using Gateway.KeyCloak.Utils;
using Gateway.Model.KeyCloak.Role;
using Infrastructure.Repositories.Setting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Gateway.KeyCloak.Services
{
    public class RoleService(IApiClient _api, IHttpContextAccessor _httpContextAccessor,IDbSettings _dbSetting) : ApiClientHelper(_httpContextAccessor, _dbSetting), IRoleService
    {
        private static string _ApiService => "/admin/realms/master/roles";

        public async Task<ApiResult<List<RoleVM>>> GetAllAsync(string search)
        {
            var url = ServiceAddress(string.Concat(_ApiService, "?search=", search));

            var headers = SetAuthorizationHeader();

            var response = await _api.GetAsync(url, headers);

            if (response.StatusCode is not HttpStatusCode.OK)
                return FaildReturn<List<RoleVM>>(response.StatusCode);

            var result = await ConvertJsonToData<List<RoleVM>>(response);

            return SuccessReturn(result);
        }

        public async Task<ApiResult<RoleVM>> GetByNameAsync(string name)
        {
            var url = ServiceAddress(string.Concat(_ApiService, "/", name));

            var headers = SetAuthorizationHeader();

            var response = await _api.GetAsync(url, headers);

            if (response.StatusCode is not HttpStatusCode.OK)
                return FaildReturn<RoleVM>(response.StatusCode);

            var result = await ConvertJsonToData<RoleVM>(response);

            return SuccessReturn(result);
        }

        public async Task<ApiResult<RoleVM>> AddAsync(AddRoleParams parameters)
        {
            var url = ServiceAddress(_ApiService);

            var headers = SetAuthorizationHeader();

            var response = await _api.PostAsync(url, parameters, headers);

            if (response is not { StatusCode: HttpStatusCode.OK or HttpStatusCode.Created })
            {
                if (response is { StatusCode: HttpStatusCode.Conflict })
                    return FaildReturn<RoleVM>(response.StatusCode, "رول از قبل موجود است", "Role exists");

                return FaildReturn<RoleVM>(response.StatusCode);
            }
            var roleAddedFindByUserName = await GetByNameAsync(parameters.Name);
            return roleAddedFindByUserName;
        }

        public async Task<ApiResult<bool>> DeleteAsync(string name)
        {
            var url = ServiceAddress(string.Concat(_ApiService, "/", name));

            var headers = SetAuthorizationHeader();

            var response = await _api.DeleteAsync(url, headers);

            if (response is not { StatusCode: HttpStatusCode.OK or HttpStatusCode.NoContent })
                return FaildReturn<bool>(response.StatusCode);


            return SuccessReturn(true);
        }

        public async Task<ApiResult<bool>> UpdateAsync(UpdateRoleParams parameters)
        {
            var url = ServiceAddress(_ApiService);

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
    }
}
