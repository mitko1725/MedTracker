using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MedTracker.Data;
using MedTracker.Models;
using MedTracker.Services.Interfaces;
using MedTracker.Services.Models;
using MedTracker.Services.Models.IdentityServiceModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedTracker.Web.Areas.Identity.Pages.Account.Manage
{
    public class Info : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityService _identity;

        public Info(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IIdentityService identity)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identity = identity;
        }




        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {


            public string FirstName { get; set; }
            public string MiddleName { get; set; }

            public string LastName { get; set; }
            [EnumDataType(typeof(Gender))]
            public Gender Gender { get; set; }
            [EnumDataType(typeof(Insured))]
            public Insured Insured { get; set; }
            public string Biography { get; set; }
            public DateTime BirthDate { get; set; }
            public string Role { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            // here should get patient or doc by Id 2 methods 
            var userInRole = await _userManager.GetRolesAsync(user);

            Input = new InputModel
            {
                Role = "Other"
            };


            if (userInRole.Contains("Doctor"))
            {
                var docDetails = _identity.GetDoctorDetails(user.Id);
                Input.FirstName = docDetails.FirstName;
                Input.MiddleName = docDetails.MiddleName;
                Input.LastName = docDetails.LastName;
                Input.Gender = docDetails.Gender;
                Input.Biography = docDetails.Biography;
                Input.Role = "Doctor";


            }
            else if (userInRole.Contains("Patient"))
            {
                var patDetails = _identity.GetPatientDetails(user.Id);
                Input.FirstName = patDetails.FirstName;
                Input.MiddleName = patDetails.MiddleName;
                Input.LastName = patDetails.LastName;
                Input.Gender = patDetails.Gender;
                Input.BirthDate = patDetails.BirthDate;
                Input.Insured = patDetails.Insured;
                Input.Role = "Patient";
            }


        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //when changing name change Name in USER and DOC/PATIENT ASWELL !


            var user = await _userManager.GetUserAsync(User);



            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            if (string.IsNullOrEmpty(Input.FirstName) || string.IsNullOrEmpty(Input.MiddleName) ||
         string.IsNullOrEmpty(Input.LastName))
            {
                StatusMessage = "Can not update empty field";
                return RedirectToPage();
            }


            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains("Patient"))
            {
                var pat = new PatientFullDetails
                {
       FirstName = Input.FirstName,
                    MiddleName = Input.MiddleName,
                    LastName = Input.LastName,
                    Gender = Input.Gender,
                    Insured = Input.Insured,
                    BirthDate = Input.BirthDate,
                    UserId=user.Id
                };
                _identity.UpdatePatientDetails(pat);
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                await _userManager.UpdateAsync(user);
                // edit method, care for validations if empty or 
                //edin user Name aswell!!
            }
            else if (userRoles.Contains("Doctor"))
            {
               
                var doc = new DoctorFullDetailsServiceModel
                {
                    FirstName = Input.FirstName,
                    MiddleName = Input.MiddleName,
                    LastName = Input.LastName,
                    Gender = Input.Gender,
                    Biography=Input.Biography,
                    UserId = user.Id
                };
                _identity.UpdateDoctorDetails(doc);
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                await _userManager.UpdateAsync(user);
        

            }
            else if (userRoles.Contains("Admin"))
            {

            }

            //if (Input.PhoneNumber != phoneNumber)
            //{
            //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            //    if (!setPhoneResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set phone number.";
            //        return RedirectToPage();
            //    }
            //}

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
