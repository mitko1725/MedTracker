using MedTracker.Services.Models.AdminServiceModels;
using MedTracker.Services.Models.DoctorSpecializationServiceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedTracker.Services.Models.AppointmentsServiceModels
{
   public class DoctorResultFromCheckboxSearchInformationServiceModel
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; }

        public IList<SpecializationDetailsServiceModel> DoctorSpecializations { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
