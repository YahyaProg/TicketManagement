using Gateway.Model.KeyCloak.Role;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Gateway.Model.KeyCloak.RoleGroup
{
    public class RoleGroupVM
    {
        #region GroupVM
        public Guid Id { get; set; }
        public string Name { get; set; }
        #endregion

        // List Of Roles
        public List<RoleVM> Roles { get; set; }
    }

    public class RoleGroupMapper
    {
        [JsonProperty("realmMappings")]
        public List<RoleVM> RealmMappings { get; set; }
    }

    public class AddRoleGroupParams
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class DeleteRoleGroupParams
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
