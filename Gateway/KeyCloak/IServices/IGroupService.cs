using Core.GenericResultModel;
using Gateway.Model.KeyCloak.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.KeyCloak.IServices
{
    public interface IGroupService
    {
        public Task<ApiResult<List<RoleKeycloakDto>>> GetAllAsync();
        public Task<ApiResult<RoleKeycloakDto>> GetAsync(Guid id);
    }
}
