using MedTracker.Services.Models.CalendarServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedTracker.Services.Interfaces
{
   public interface ICalendarService
    {
        public Task<IEnumerable<AppointmentDetailsServiceModels>> GetCurrentDoctorAppointmentsForThisWeek(DateTime startDate, DateTime endDate, int docId);
    }
}
