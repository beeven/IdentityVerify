using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IdentityVerifyLib.Data
{
    public class AuthRequest
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("cert_mode")]
        public int Mode { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("id_num")]
        public string IdNumber { get; set; }
        [JsonProperty("portrait_base64")]
        public string Portrait { get; set; }
    }
}
