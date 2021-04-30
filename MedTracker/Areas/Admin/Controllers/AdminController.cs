using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedTracker.Services.Interfaces;
using MedTracker.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MedTracker.Web.Data.Services.Interfaces;
using MedTracker.Web.Areas.Admin.ViewModels.AdminModels;

namespace MedTracker.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _admin;
        private readonly IAdminUserService _UserAdmin;


        public AdminController(IAdminService admin, IAdminUserService UserAdmin)
        {
            _admin = admin;
            _UserAdmin = UserAdmin;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Paging is not Fixed!!
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public IActionResult ApproveDoctors(int page = 1)
        {
            try
            {
                var doctors = _admin.NotApprovedDoctors(page);
                var userDoctors = _UserAdmin.NotApprovedUserDoctors(doctors);
                var totalDoctors = _admin.TotalNotActive();
                var maxPage = Math.Ceiling((double)totalDoctors / 20);

                var model = new NotActiveDoctorsViewModel()
                {
                    Doctors = doctors,
                    UserDoctors = userDoctors,
                    Total = totalDoctors,
                    CurrentPage = page
                };
                if (page > maxPage)
                {
                    throw new Exception("Max Page is exceeded");
                }
                return View(model);
            }
            catch {
                var model = new NotActiveDoctorsViewModel()
                {
                    CurrentPage = 1,
                    Total = 1
                };

                return View(model);

            }
        }


    
        public IActionResult Details(Guid Id)
        {
            var doc = _admin.FindDoctorById(Id);
            var userDoc = _UserAdmin.FindUserDoctorById(doc);
            //should display doctor specializations here 
            var docSpecs = _admin.DoctorSpecializations(doc.Id);
            var viewModelForDetails = new DetailsDoctorViewModel()
            {
                Doctor = doc,
                UserDoctor=userDoc,
                DocSpecs= docSpecs
            };

            return View(viewModelForDetails);
        }

     
        public IActionResult Approve(Guid userId)
        {
            var doc = _admin.FindDoctorById(userId);
          
            _admin.MakeDoctorActive(doc);
        
            return RedirectToAction("ApproveDoctors");
        }

       
        public IActionResult Reject(Guid userId)
        {
            var doc = _admin.FindDoctorById(userId);
            //shoud remove doctor specs here ! 
            doc.DoctorSpecializations = _admin.ListOfDoctorSpecializations(doc.Id).ToList();
            // add list of docSpecs and add id to method
            _UserAdmin.RejectUserDoctor(doc);
            return RedirectToAction("ApproveDoctors");
          
        }


        public IActionResult Specializations(int page = 1)
        {
            try
            {
                var specs = _admin.AllSpecializations(page);

                var totalSpecs = _admin.TotalSpecializations();
                var maxPage = Math.Ceiling((double)totalSpecs / 20);
                var model = new SpecializationViewModel()
                {
                    Specializations = specs,
                    Total = totalSpecs,
                    CurrentPage = page
                };
                if (page > maxPage)
                {
                    throw new Exception("Max Page is exceeded");
                }
                return View(model);
            }
            catch {

                var model = new SpecializationViewModel()
                {
                    CurrentPage = 1,
                    Total = 1
                };

                return View(model);


            }

        }
        public IActionResult AddSpecialization()
        {

            return View();

        }
        [HttpPost]
        public IActionResult AddSpecialization(string specName)
        {

      
            _admin.AddSpecialization(specName);
            return RedirectToAction("Specializations");

        }

     
        public IActionResult RemoveSpecialization(int id)
        {

            _admin.RemoveSpecialization(id);
            
            return RedirectToAction("Specializations");

        }

    }
}
