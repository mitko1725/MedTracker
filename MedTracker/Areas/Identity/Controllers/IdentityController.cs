using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedTracker.Data;
using MedTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedTracker.Web.Areas.Identity.Controllers
{
  [AllowAnonymous]
    [Area("Identity")]
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identity;
        private readonly UserManager<ApplicationUser> _userManager;
        private Dictionary<int, string> DocSpecializationsSelect { get; set; } = new Dictionary<int, string>();
        public IdentityController(IIdentityService identity, UserManager<ApplicationUser> userManager)
        {
            _identity = identity;
            _userManager = userManager;
        }
     
        public IActionResult AccountNotActivated()
        {
            return View();
        }
        [HttpGet("Test")]
        public JsonResult Test()
        {
            /// fill select from ehre 
     
            return Json(new { foo = "bar" });
        }


    }
}
