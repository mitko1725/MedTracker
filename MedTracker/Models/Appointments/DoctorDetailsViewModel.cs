using MedTracker.Services.Models;
using MedTracker.Services.Models.IdentityServiceModels;
using MedTracker.Web.Data.Models.AdminUserServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedTracker.Web.Models.Appointments
{
    public class DoctorDetailsViewModel
    {
        public DoctorFullDetailsServiceModel DoctorDetails { get; set; }
        public UserDoctorFullDetailsServiceModel UserDoctorDetails { get; set; }
        public IEnumerable<ModelsForDoctorSpecializationsServiceModel> DoctorsSpecializations { get; set; }
    }
}
