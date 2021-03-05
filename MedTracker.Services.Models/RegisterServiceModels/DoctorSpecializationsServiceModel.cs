using System;
using System.Collections.Generic;
using System.Text;

namespace MedTracker.Services.Models.RegisterServiceModels
{
  public  class DoctorSpecializationsServiceModel
    {
        public int DoctorId { get; set; }
        public ICollection<int> DoctorSpecializations{ get; set; }
    }
}
