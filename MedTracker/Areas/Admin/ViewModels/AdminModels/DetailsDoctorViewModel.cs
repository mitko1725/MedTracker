using MedTracker.Services.Models;
using MedTracker.Web.Data.Models.AdminUserServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedTracker.Web.Areas.Admin.ViewModels
{
    public class DetailsDoctorViewModel
    {
        public DoctorFullDetailsServiceModel Doctor { get; set; }

        public UserDoctorFullDetailsServiceModel UserDoctor { get; set; }

    }
}
