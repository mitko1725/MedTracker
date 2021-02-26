using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedTracker.Web.Areas.Identity.Controllers
{
  [AllowAnonymous]
    [Area("Identity")]
    public class IdentityController : Controller
    {
        public IdentityController()
        {

        }
     
        public IActionResult AccountNotActivated()
        {
            return View();
        }
    

    }
}
