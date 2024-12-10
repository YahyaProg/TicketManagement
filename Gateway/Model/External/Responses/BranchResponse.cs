using System.Collections.Generic;

namespace Gateway.Model.External.Responses
{
    public class BranchResponse
    {
        public BranchModel Branch { get; set; }
        public List<BranchModel> RelatedBranch { get; set; }
    }


    public class BranchModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
