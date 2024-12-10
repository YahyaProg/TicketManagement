using Core.GenericResultModel;
using Gateway.Model.KeyCloak.Role;
using Gateway.Model.KeyCloak.RoleGroup;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.KeyCloak.IServices
{
    public interface IRoleGroupService
    {
        public Task<ApiResult<List<RoleVM>>> GetAsync(Guid id);
        public Task<ApiResult<bool>> AddAsync(List<AddRoleGroupParams> parameters, Guid groupId);
        public Task<ApiResult<bool>> DeleteAsync(List<DeleteRoleGroupParams> parameters, Guid groupId);
    }
}
