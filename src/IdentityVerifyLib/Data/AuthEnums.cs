using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityVerifyLib.Data
{
    public enum CertMode
    {
        Normal,
        WithPortrait
    }

    public enum IdMatchResult
    {
        Valid,
        Invalid,
        NotFound,
        ArgumentInvalid,
        Errored,

    }
    public enum PortraitMatchResult
    {
        Identical,
        NotIdentical,
        Similar,
        Errored,
        UnsupportedImageFormat,
        FaceNotFound,
        UnableToCompare,
        ImageTooSmall
    }

    public static class AuthResultMap
    {
        public static IDictionary<char, IdMatchResult> IdMatchResultDic;
        public static IDictionary<char, PortraitMatchResult> PortraitMatchResultDic;
        static AuthResultMap()
        {
            IdMatchResultDic = new Dictionary<char, IdMatchResult>()
            {
                {'0',IdMatchResult.Valid },
                {'4',IdMatchResult.Invalid },
                {'5', IdMatchResult.NotFound },
                {'6', IdMatchResult.ArgumentInvalid },
                {'7', IdMatchResult.Errored }
            };

            PortraitMatchResultDic = new Dictionary<char, PortraitMatchResult>()
            {
                {'0', PortraitMatchResult.Identical },
                {'3', PortraitMatchResult.Identical },
                {'1', PortraitMatchResult.NotIdentical },
                {'4', PortraitMatchResult.NotIdentical },
                {'2', PortraitMatchResult.Similar },
                {'5', PortraitMatchResult.Similar },
                {'A', PortraitMatchResult.Errored },
                {'B', PortraitMatchResult.Errored },
                {'C', PortraitMatchResult.Errored },
                {'D', PortraitMatchResult.Errored },
                {'H', PortraitMatchResult.Errored },
                {'I', PortraitMatchResult.Errored },
                {'K', PortraitMatchResult.Errored },
                {'W', PortraitMatchResult.Errored },
                {'E', PortraitMatchResult.UnsupportedImageFormat },
                {'F', PortraitMatchResult.FaceNotFound },
                {'G', PortraitMatchResult.UnableToCompare },
                {'J', PortraitMatchResult.ImageTooSmall },
                {'X', PortraitMatchResult.Errored }
            };
        }
    }
}
