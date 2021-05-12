using MedTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedTracker.Services.Models.CalendarServiceModels
{
  public  class AppointmentDetailsServiceModels
    {

        public Guid Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public AppReason Reason { get; set; }
    }
}
