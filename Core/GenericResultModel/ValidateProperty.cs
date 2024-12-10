using System.Collections.Generic;

namespace Core.GenericResultModel
{
    public class ValidateProperty
    {
        public string PropertyName { get; set; }
        public List<string> Errors { get; set; }
    }
}