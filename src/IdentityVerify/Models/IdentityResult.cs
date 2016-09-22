using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using IdentityVerifyLib.Data;

namespace IdentityVerify.Models
{
    public class IdentityResult
    {
        public bool IsSuccessful { get; set; }
        public string IdMatchResult {get;set;}
        public string PortraitMatchResult { get; set; }

        public static IdentityResult FromAuthResult(AuthResult ar)
        {
            var ret = new IdentityResult();
            switch(ar.IdMatchResult)
            {
                case IdentityVerifyLib.Data.IdMatchResult.Valid:
                    ret.IdMatchResult = "身份信息有效";
                    break;
                case IdentityVerifyLib.Data.IdMatchResult.Invalid:
                    ret.IdMatchResult = "身份信息无效";
                    break;
                case IdentityVerifyLib.Data.IdMatchResult.NotFound:
                    ret.IdMatchResult = "身份信息未查到";
                    break;
                case IdentityVerifyLib.Data.IdMatchResult.ArgumentInvalid:
                    ret.IdMatchResult = "身份信息数据长度不正确";
                    break;
                case IdentityVerifyLib.Data.IdMatchResult.Errored:
                    ret.IdMatchResult = "身份信息系统错误";
                    break;
                default:
                    ret.IdMatchResult = "身份信息系统错误";
                    break;
            }
            switch(ar.PortraitMatchResult)
            {
                case IdentityVerifyLib.Data.PortraitMatchResult.Identical:
                    ret.PortraitMatchResult = "同一人";
                    break;
                case IdentityVerifyLib.Data.PortraitMatchResult.NotIdentical:
                    ret.PortraitMatchResult = "非同一人";
                    break;
                case IdentityVerifyLib.Data.PortraitMatchResult.Similar:
                    ret.PortraitMatchResult = "疑似同一人";
                    break;
                case IdentityVerifyLib.Data.PortraitMatchResult.Errored:
                    ret.PortraitMatchResult = "人像对比执行失败";
                    break;
                case IdentityVerifyLib.Data.PortraitMatchResult.UnsupportedImageFormat:
                    ret.PortraitMatchResult = "图像格式不支持";
                    break;
                case IdentityVerifyLib.Data.PortraitMatchResult.FaceNotFound:
                    ret.PortraitMatchResult = "面部图像没有找到";
                    break;
                case IdentityVerifyLib.Data.PortraitMatchResult.UnableToCompare:
                    ret.PortraitMatchResult = "身份证照片不存在，无法对比";
                    break;
                case IdentityVerifyLib.Data.PortraitMatchResult.ImageTooSmall:
                    ret.PortraitMatchResult = "照片太小";
                    break;
            }
            ret.IsSuccessful = ar.IsSuccessful;


            return ret;
        }
    }

}