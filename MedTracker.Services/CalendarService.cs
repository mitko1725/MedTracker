using MedTracker.Data;
using MedTracker.Services.Interfaces;
using MedTracker.Services.Models.CalendarServiceModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedTracker.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly MedTrackerDbContext data;


        public CalendarService(MedTrackerDbContext data)
        {
            this.data = data;
        }

        public async Task<IEnumerable<AppointmentDetailsServiceModels>> GetCurrentDoctorAppointmentsForThisWeek(DateTime startDate, DateTime endDate, int docId)
        {
            var doctorAppsForCurrWeek = await this.data.Appointments.AsNoTracking()
                .Where(x => x.DoctorId == docId)
                .Select(x => new AppointmentDetailsServiceModels()
                {
                    Id = x.Id,
                    DoctorId = x.DoctorId,
                    PatientId = x.PatientId,
                    Reason = x.Reason,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate

                })
                .ToListAsync();


            return doctorAppsForCurrWeek;
        }

    
    }
}
