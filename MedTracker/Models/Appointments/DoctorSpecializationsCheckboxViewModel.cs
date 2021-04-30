

using MedTracker.Services.Models.AppointmentsServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedTracker.Web.Models
{
    public class DoctorSpecializationsCheckboxViewModel
    {
     
        public IList<DoctorSpecializationsCheckboxServiceModel> DoctorSpecializationsCheckBoxes { get; set; }
    }

 
}
