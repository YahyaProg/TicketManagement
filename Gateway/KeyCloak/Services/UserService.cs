using Core.GenericResultModel;
using Gateway.KeyCloak.IServices;
using Gateway.KeyCloak.Utils;
using Gateway.Model.KeyCloak.Params;
using Gateway.Model.KeyCloak.ViewModel;
using Infrastructure.Repositories.Setting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Gateway.KeyCloak.Services
{
    public class UserService(IApiClient _api, IHttpContextAccessor _httpContextAccessor, IDbSettings _dbSetting, IAuthService _authService)
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
        : ApiClientHelper(_httpContextAccessor, _dbSetting), IUserService
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
    {

        private static string _ApiService => "/admin/realms/master/users";

        public async Task<ApiResult<UserKeyCloakVM>> AddAsync(AddUserKeyCloakParams parameters)
        {
            var url = ServiceAddress(_ApiService);

            var headers = SetAuthorizationHeader();

            var response = await _api.PostAsync(url, parameters, headers);

            if (response is not { StatusCode: HttpStatusCode.OK or HttpStatusCode.Created })
            {
                if (response is { StatusCode: HttpStatusCode.Conflict })
                    return FaildReturn<UserKeyCloakVM>(response.StatusCode, "کاربر از قبل موجود است", "User exists");

                return FaildReturn<UserKeyCloakVM>(response.StatusCode);
            }
            var userAddedFindByUserName = await FindByUserNameAsync(parameters.username);
            return userAddedFindByUserName;
        }

        public async Task<ApiResult<UserKeyCloakVM>> FindByUserNameAsync(string username)
        {
            var url = ServiceAddress(string.Concat(_ApiService, "?username=", username));

            var headers = SetAuthorizationHeader();

            var response = await _api.GetAsync(url, headers);

            if (response.StatusCode is not HttpStatusCode.OK)
                return FaildReturn<UserKeyCloakVM>(response.StatusCode);

            var result = await ConvertJsonToData<List<UserKeyCloakVM>>(response);
            return SuccessReturn(result.FirstOrDefault());
        }

        public async Task<ApiResult<UserKeyCloakVM>> FindByIdAsync(Guid id)
        {
            var url = ServiceAddress(string.Concat(_ApiService, "/", id));

            var headers = SetAuthorizationHeader();

            var response = await _api.GetAsync(url, headers);

            if (response.StatusCode is not HttpStatusCode.OK)
                return FaildReturn<UserKeyCloakVM>(response.StatusCode);

            var result = await ConvertJsonToData<UserKeyCloakVM>(response);
            return SuccessReturn(result);
        }

        public async Task<ApiResult<bool>> ChangePasswordAsync(Guid userId, string password)
        {
            var url = ServiceAddress(string.Concat(_ApiService, "/", userId, "/reset-password"));
            var adminToken = await _authService
                .Login(_dbSetting.GetSetting("admin_username"), _dbSetting.GetSetting("admin_password"));
            var headers = SetAuthorizationHeader();
            headers.Clear();
            headers.Add("Authorization", $"Bearer {adminToken.access_token}");
            var response = await _api.PutAsync(url, new { value = password }, headers);

            if (response is not { StatusCode: HttpStatusCode.OK or HttpStatusCode.NoContent })
                return FaildReturn<bool>(response.StatusCode);

            return SuccessReturn(true);
        }

        public async Task<ApiResult<bool>> ChangeStatusAsync(Guid? id, bool? status)
        {
            var url = ServiceAddress(string.Concat(_ApiService, "/", id));

            var headers = SetAuthorizationHeader();

            var response = await _api.PutAsync(url, new { enabled = status }, headers);

            if (response is not { StatusCode: HttpStatusCode.OK or HttpStatusCode.NoContent })
                return FaildReturn<bool>(response.StatusCode);

            return SuccessReturn(true);
        }

        public async Task<ApiResult<bool>> UpdateAsync(UpdateUserKeyCloakParams parameters)
        {
            var url = ServiceAddress(string.Concat(_ApiService, "/", parameters.id));

            var headers = SetAuthorizationHeader();

            var response = await _api.PutAsync(url, parameters, headers);

            if (response is not { StatusCode: HttpStatusCode.OK or HttpStatusCode.NoContent})
                return FaildReturn<bool>(response.StatusCode);

            return SuccessReturn(true);

        }
    }
}
