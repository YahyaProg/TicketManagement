using Core.GenericResultModel;
using Gateway.Model.KeyCloak.Role;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.KeyCloak.IServices
{
    public interface IRoleService
    {
        public Task<ApiResult<List<RoleVM>>> GetAllAsync(string search);
        public Task<ApiResult<RoleVM>> GetByNameAsync(string name);
        public Task<ApiResult<RoleVM>> AddAsync(AddRoleParams parameters);
        public Task<ApiResult<bool>> DeleteAsync(string name);
        public Task<ApiResult<bool>> UpdateAsync(UpdateRoleParams parameters);
    }
}
