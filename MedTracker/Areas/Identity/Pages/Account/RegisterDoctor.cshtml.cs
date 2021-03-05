using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using MedTracker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using MedTracker.Services.Interfaces;
using MedTracker.Models;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using MedTracker.Services.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using MedTracker.Services.Models.RegisterServiceModels;

namespace MedTracker.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterDoctor : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRegisterService _register;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IIdentityService _identity;


        public RegisterDoctor(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IRegisterService register, IWebHostEnvironment webHostEnvironment, IIdentityService identity)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _register = register;
            _webHostEnvironment = webHostEnvironment;
            _identity = identity;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public Dictionary<int, string> DocSpecializations { get; set; } = new Dictionary<int, string>();

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [DisplayName("First Name")]
            public string FirstName { get; set; }

            public string MiddleName { get; set; }
            [Required]
            [DisplayName("Last Name")]
            public string LastName { get; set; }
            [Required]
            public string PhoneNumber { get; set; }

            public string Biography { get; set; }


            public IList<int> SelectedSpecializations { get; set; }



            [Required]
            public Gender Gender { get; set; }
            [Display(Name = "Profile picture")]
            public IFormFile ProfilePicToSave { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            FillViewBagWithDataForSelectInView();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null, int gender = -1)
        {

            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = Input.Email, Email = Input.Email, FirstName = Input.FirstName, LastName = Input.LastName, PhoneNumber = Input.PhoneNumber };

                var result = await _userManager.CreateAsync(user, Input.Password);



                if (result.Succeeded)
                {
                    var user1 = await _userManager.FindByEmailAsync(user.Email);

                    var doctor = new CreateDoctorServiceModel()
                    {
                        FirstName = Input.FirstName,
                        MiddleName = Input.MiddleName,
                        LastName = Input.LastName,
                        Gender = (Gender)gender,
                        Biography = Input.Biography,
                        ProfilePic = UploadedFile(),
                        UserId = user1.Id
                    };
                    await _userManager.AddToRoleAsync(user1, "Doctor");
                    _register.CreateDoctor(doctor);
                    var getInfo = _register.GetDoctorByUserId(user1.Id);
                    var docSpecs = new DoctorSpecializationsServiceModel()
                    {
                        DoctorId = getInfo.Id,
                        DoctorSpecializations = Input.SelectedSpecializations

                    };
                    _register.AddDoctorSpecializations(docSpecs);
                    //create doc_spec
                    //service method that contains Doc ID and COllections of ints with specilizations
                    //method VOID



                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {

                        return RedirectToAction("AccountNotActivated", "Identity");
                        //await _signInManager.SignInAsync(user, isPersistent: false);

                        //return Redirect("/Views/AccountNotActivated");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        private string UploadedFile()
        {
            // trqbva v onpost methoda 
            string uniqueFileName = null;
            //ако е нямал снимка но да не гърми
            if (Input.ProfilePicToSave != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Input.ProfilePicToSave.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Input.ProfilePicToSave.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public void OnPostYogaPostures(int sessionCount)
        {
            //do your work here


        }

        [NonAction]
        private void FillViewBagWithDataForSelectInView()
        {
            var modelDataForViewBag = _identity.ModelsDoctorSelect();
            foreach (var item in modelDataForViewBag)
            {
                DocSpecializations.Add(item.Id, item.Name);
            }

        }

    }
}