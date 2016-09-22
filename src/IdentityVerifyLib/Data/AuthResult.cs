using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityVerifyLib.Data
{
    public class AuthResult
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public IdMatchResult IdMatchResult { get; set; }
        public PortraitMatchResult PortraitMatchResult { get; set; }
    }
}
