using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MedTracker.Data;
using MedTracker.Models;
using MedTracker.Services.Interfaces;
using MedTracker.Services.Models.IdentityServiceModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedTracker.Web.Areas.Identity.Pages.Account.Manage
{
    public class DoctorSpecializations : PageModel
    {


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityService _identity;

        public DoctorSpecializations(
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

        public IEnumerable<ModelsForDoctorSpecializationsServiceModel> SpecializationsForSelect { get; set; }


     


        public class InputModel
        {


            public IEnumerable<ModelsForDoctorSpecializationsServiceModel> Specializations { get; set; }






        }

        public async Task<IActionResult> OnPostAddNewSpecialization(List<int> model)
        {
            var user = await _userManager.GetUserAsync(User);
            var doctorInfo = _identity.GetDoctorDetails(user.Id);
            //method that adds specs to the doc 
            // add in Doc_Spec tables

                _identity.AddNewSpecializationsForDoctor(model, doctorInfo.Id);

            await LoadAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            FillViewBagWithDataForSelectInView();
            var getDoctor = _identity.GetDoctorDetails(user.Id);

            var doctorSpecDetails = _identity.DoctorSpecializations(getDoctor.Id);
         
            Input = new InputModel
            {
                Specializations = doctorSpecDetails
            };

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


        public async Task<IActionResult> OnPostAsync(int docSpecId)
        {
            //should remove doc spec here 
            var user = await _userManager.GetUserAsync(User);
            var doctorInfo = _identity.GetDoctorDetails(user.Id);

            _identity.RemoveDoctorSpecialization(docSpecId, doctorInfo.Id);


            await LoadAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();

        }


        [NonAction]
        private async void FillViewBagWithDataForSelectInView()
        {
            var user = await _userManager.GetUserAsync(User);
            var doctor = _identity.GetDoctorDetails(user.Id);


            var docCurrentSpecs = _identity.DoctorSpecializations(doctor.Id);


            // get Doctor current specializations
            var modelsForViewBag = _identity.DoctorSpecializationThatCanBeAddedForSelect(docCurrentSpecs);
            SpecializationsForSelect = modelsForViewBag.Select(x => new ModelsForDoctorSpecializationsServiceModel
            {
                Id = x.Id,
                Name = x.Name
            });

        }


    }
}
