using System.Collections.Generic;

namespace Gateway.Model.External.Responses
{
    public class BranchUserResponse
    {
        public BranchModel Branch { get; set; }
        public List<BranchUser> BranchUsersList { get; set; }
    }

    public class BranchUser
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Enabled { get; set; }
        public List<RelatedRole> RoleRelations { get; set; }
    }

    public class RelatedRole
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }
}
