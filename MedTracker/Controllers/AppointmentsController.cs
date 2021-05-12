using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using MedTracker.Services.Interfaces;
using MedTracker.Web.Models.Appointments;
using MedTracker.Web.Data.Services.Interfaces;
using MedTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace MedTracker.Web.Controllers
{

    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsService _appointments;
        //used to Find Doctor Specializations
        private readonly IIdentityService _identity;

        //used to Find Doctor Details UserDoctorDetails
        private readonly IAdminUserService _adminUser;
        private readonly MedTracker.Data.MedTrackerDbContext _context;



        public AppointmentsController(IAppointmentsService appointments, IIdentityService identity, IAdminUserService adminUser, MedTracker.Data.MedTrackerDbContext context)
        {
            _appointments = appointments;
            _identity = identity;
            _adminUser = adminUser;
            _context = context;
        }
        public IActionResult Index()
        {
            var getDoctorSpecsCheckbox = _appointments.ListOfDoctorSpecializationsForCheckboxes();



            var viemModel = new DoctorsResultFromSearchViewModel()
            {
                DoctorSpecializationsCheckBoxes = getDoctorSpecsCheckbox.ToList(),

            };

            return View(viemModel);
        }



        [HttpPost]
        public IActionResult Index(DoctorSpecializationsCheckboxViewModel givm, int page = 1)
        {


            if (!ModelState.IsValid)
            {
                throw new ArgumentException();
            }
            try
            {

                var doctorsForSearchResult = _appointments.ResultFromSearchCheckboxesForDoctors(givm.DoctorSpecializationsCheckBoxes).ToList();
                int totalDoctors = _appointments.TotalDoctorsFromSearch;
                var maxPage = Math.Ceiling((double)totalDoctors / 20);
                if (page > maxPage)
                {
                    throw new Exception("Max Page is exceeded");
                }
                var model = new DoctorsResultFromSearchViewModel
                {
                    DoctorSpecializationsCheckBoxes = givm.DoctorSpecializationsCheckBoxes,
                    Doctors = doctorsForSearchResult,
                    Total = totalDoctors,
                    CurrentPage = page

                };
                //ostana viewto da se napravi 
                return View(model);

            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }



        }


        public IActionResult MakeAppointment(int doctorId)
        {
            //tuk trqbva da se dobavi viewModel s informaciqta za currentDoctor i appointments calendara!

            //make viewModel and add doctorSpecializations from Id
            var viewModel = new DoctorDetailsViewModel()
            {
                DoctorDetails= _appointments.GetDoctorDetailsById(doctorId),
                DoctorsSpecializations = _identity.DoctorSpecializations(doctorId),
                
            };
            viewModel.UserDoctorDetails = _adminUser.FindUserDoctorById(viewModel.DoctorDetails);
            return View(viewModel);

        }


    

    }
}
