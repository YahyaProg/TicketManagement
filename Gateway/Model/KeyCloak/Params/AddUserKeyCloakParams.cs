using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Gateway.Model.KeyCloak.Params
{
    public class CredentialsKeyCloak
    {
        public string value { get; set; }
        public bool temporary { get; set; }
    }
    public class AttributesKeyCloak
    {
        public long? OrganizationId { get; set; }
    }
    public class AddUserKeyCloakParams
    {
#nullable enable
        public string? username { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public List<string>? groups { get; set; }
        public bool enabled { get; set; }
        // For Password Keycloak
        public List<CredentialsKeyCloak>? credentials { get; set; }
        public AttributesKeyCloak? attributes { get; set; }
    }

    public class UpdateUserKeyCloakParams
    {
        public Guid id { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public bool enabled { get; set; }
        public AttributesKeyCloak? attributes { get; set; }
    }
}
