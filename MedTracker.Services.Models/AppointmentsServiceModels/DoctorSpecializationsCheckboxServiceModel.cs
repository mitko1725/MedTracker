using System;
using System.Collections.Generic;
using System.Text;

namespace MedTracker.Services.Models.AppointmentsServiceModels
{
   public class DoctorSpecializationsCheckboxServiceModel
    {
        public int SpecId { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}
