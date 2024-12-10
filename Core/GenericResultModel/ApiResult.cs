using System.Collections.Generic;

namespace Core.GenericResultModel
{
    public class ApiResult(int code = 200, bool isSuccess = true) : ApiResult<string>(code, isSuccess)
    { }

    public class ApiResult<T>(int code = 200, bool isSuccess = true)
    {
        public bool IsSuccess { get; set; } = isSuccess;

        private string _message;
        public string Message
        {
            set { _message = value; }
            get
            {
                if (!string.IsNullOrEmpty(_message))
                    return _message;
                else
                    return ResourceConfig.ResourcesFa.GetString(Code.ToString());
            }
        }

        private string _messageEn;
        public string MessageEn
        {
            set { _messageEn = value; }
            get
            {
                if (!string.IsNullOrEmpty(_messageEn))
                    return _messageEn;
                else
                    // ResourceManager rm = new ResourceManager("Model.Resources.Resource-en", Assembly.GetExecutingAssembly());
                    // if (rm != null && ErrorCode != null && ErrorCode > 0 && !String.IsNullOrEmpty(rm.GetString(ErrorCode.ToString())))
                    //     return rm.GetString(ErrorCode.ToString());
                    return ResourceConfig.ResourcesEn.GetString(Code.ToString());
            }
        }

        public T Data { get; set; }
        public List<ValidateProperty> ValidationErrors { get; set; }

        public int Code
        {
            get => code;
            set { code = value; }
        }
    }

}