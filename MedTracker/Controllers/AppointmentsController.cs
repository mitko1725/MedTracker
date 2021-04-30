using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using MedTracker.Services.Interfaces;
using MedTracker.Web.Models.Appointments;

namespace MedTracker.Web.Controllers
{

    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsService _appointments;

        public AppointmentsController(IAppointmentsService appointments)
        {
            _appointments = appointments;
        }
        public IActionResult Index()
        {
            var getDoctorSpecsCheckbox = _appointments.ListOfDoctorSpecializationsForCheckboxes();

        

            var viemModel = new DoctorsResultFromSearchViewModel()
            {
                DoctorSpecializationsCheckBoxes = getDoctorSpecsCheckbox.ToList()
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
    }
}
