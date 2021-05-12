﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MedTracker.Models
{
  public  class Appointment
    {
        [Key]
        public Guid Id { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public AppReason Reason { get; set; }

    }
}
