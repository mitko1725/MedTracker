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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MedTracker.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterUser : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRegisterService _register;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IIdentityService _identity;

        public RegisterUser(
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
          
            public string PhoneNumber { get; set; }
            [Required]
            public DateTime Birthdate { get; set; }
            [Required]
            public Gender Gender { get; set; }
            [Required]
            public Insured Insured { get; set; }

            public string ProfilePic { get; set; }


            [Display(Name = "Profile picture")]
            public IFormFile ProfilePicToSave { get; set; }




        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

        
           


            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null, int gender=-1,int insured=-1)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = Input.Email, Email = Input.Email,FirstName=Input.FirstName,LastName=Input.LastName,PhoneNumber=Input.PhoneNumber };
          
                var result = await _userManager.CreateAsync(user, Input.Password);
                ///TO add my method to add the user with that Id and Details
                if (result.Succeeded)
                {
                    var user1 = await _userManager.FindByEmailAsync(user.Email);
              
                    var patient = new CreateUserServiceModel()
                    {
                        FirstName = Input.FirstName,
                        MiddleName = Input.MiddleName,
                        LastName = Input.LastName,
                        Birthdate = Input.Birthdate,
                        Gender = (Gender)gender,
                        Insured = (Insured)insured,
                        ProfilePic = UploadedFile(),
                        UserId = user1.Id
                    };
                 
                    await  _userManager.AddToRoleAsync(user1, "Patient");
                    _register.CreateUser(patient);
                  


                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
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

    }
}