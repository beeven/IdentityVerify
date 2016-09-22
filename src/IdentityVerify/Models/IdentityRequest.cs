using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityVerify.Models
{
    public class IdentityRequest
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Portrait { get; set; }
    }
}
