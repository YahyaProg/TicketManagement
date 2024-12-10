using System.Collections.Generic;

namespace Gateway.Model
{
    public class FluentResultVm<TValue>
    {
        public bool IsFailed { get; set; }
        public bool IsSuccess { get; set; }
        public List<Reason> Reasons { get; set; }
        public List<Error> Errors { get; set; }
        public List<Success> Successes { get; set; }
        public TValue ValueOrDefault { get; set; }
        public TValue Value { get; set; }
    }



    public class Reason
    {
        public string Message { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class Metadata
    {
        public string AdditionalProp1 { get; set; }
        public string AdditionalProp2 { get; set; }
        public string AdditionalProp3 { get; set; }
    }
    public class Error
    {
        public string Message { get; set; }
        public Metadata Metadata { get; set; }
        public string[] Reasons { get; set; }
    }

    public class Success
    {
        public string Message { get; set; }
        public Metadata Metadata { get; set; }
    }
}
