using Core.GenericResultModel;
using Gateway.Model.KeyCloak.Params;
using Gateway.Model.KeyCloak.ViewModel;
using System;
using System.Threading.Tasks;

namespace Gateway.KeyCloak.IServices
{
    public interface IUserService
    {
        public Task<ApiResult<bool>> ChangeStatusAsync(Guid? id, bool? status);
        public Task<ApiResult<UserKeyCloakVM>> AddAsync(AddUserKeyCloakParams parameters);
        public Task<ApiResult<bool>> UpdateAsync(UpdateUserKeyCloakParams parameters);
        public Task<ApiResult<UserKeyCloakVM>> FindByUserNameAsync(string username);
        public Task<ApiResult<UserKeyCloakVM>> FindByIdAsync(Guid id);
        public Task<ApiResult<bool>> ChangePasswordAsync(Guid userId, string password);
    }
}
