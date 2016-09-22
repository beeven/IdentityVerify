using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityVerifyLib.Data;

namespace IdentityVerifyLib
{
    public interface IIdentityVerifyService
    {
        Task<AuthResult> VerifyIdAsync(string idNum, string fullName);
        AuthResult VerifyId(string idNum, string fullName);
        Task<AuthResult> VerifyIdWithImageAsync(string idNum, string fullName, string image);
        AuthResult VerifyIdWithImage(string idNum, string fullName, string image);
    }
}
