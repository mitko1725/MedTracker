using System;
using System.Collections.Generic;
using System.Text;

namespace MedTracker.Models
{
  public  class Doctor_Specialization
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}
