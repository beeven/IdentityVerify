using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityVerify.Models;
using IdentityVerifyLib;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityVerify.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IIdentityVerifyService verifyService;
        public IdentityController(IIdentityVerifyService verifyService)
        {
            this.verifyService = verifyService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyResult(IdentityRequest model)
        {
            IdentityVerifyLib.Data.AuthResult result;
            if(!String.IsNullOrEmpty(model.Portrait))
            {
                result = await verifyService.VerifyIdWithImageAsync(model.ID, model.FullName, model.Portrait.Substring(model.Portrait.IndexOf(",") + 1));
            }
            else
            {
                result = await verifyService.VerifyIdAsync(model.ID, model.FullName);
            }
            
            
            
            return View(IdentityResult.FromAuthResult(result));
        }
    }
}
