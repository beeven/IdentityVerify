using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityVerifyLib
{
    public class IdentifyVerifyLibException: Exception
    {
        public IdentifyVerifyLibException() : base() { }
        public IdentifyVerifyLibException(string message) : base(message) { }
    }

    public class AccessTokenException:IdentifyVerifyLibException
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public AccessTokenException() : base() { }
        public AccessTokenException(string message) : base(message) { this.ErrorMessage = message; }
        public AccessTokenException(int errorCode)
        {
            ErrorCode = errorCode;
            if(ErrorCodes.ErrorCodeDic.ContainsKey(errorCode))
            {
                ErrorMessage = ErrorCodes.ErrorCodeDic[errorCode];
            }
        }
        public AccessTokenException(int errorCode, string errorMessage):base(errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
        }


    }

    public static class ErrorCodes
    {
        public static IDictionary<int, string> ErrorCodeDic;
        static ErrorCodes()
        {
            ErrorCodeDic = new Dictionary<int, string>()
            {
                {1,"身份认证请求失败" },
                {2, "服务器错误" },
                {4002, "参数错误" },
                {4101, "身份认证结果不匹配" },
                {4102, "不支持的认证模式" },
                {4103, "身份认证服务异常" }
            };
        }
    }


}
