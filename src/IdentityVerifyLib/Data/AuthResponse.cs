using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IdentityVerifyLib.Data
{
    public class AuthResponse
    {
        [JsonProperty("ret_code")]
        public int ReturnCode { get; set; }

        [JsonProperty("error_msg")]
        public string ErrorMessage { get; set; }

        [JsonProperty("cert_str")]
        public string  Result { get; set; }

        public AuthResult ToAuthResult()
        {
            AuthResult result = new AuthResult();
            result.IsSuccessful = this.ReturnCode == 0;
            result.ErrorMessage = this.ErrorMessage;
            if(!String.IsNullOrEmpty(Result))
            {
                if (AuthResultMap.IdMatchResultDic.ContainsKey(Result[0]))
                    result.IdMatchResult = AuthResultMap.IdMatchResultDic[this.Result[0]];
                else
                    result.IdMatchResult = IdMatchResult.Errored;
                result.PortraitMatchResult = AuthResultMap.PortraitMatchResultDic[this.Result[1]];
            }
            return result;
        }
    }

    

}
