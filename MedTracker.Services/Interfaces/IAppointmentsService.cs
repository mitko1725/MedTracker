using MedTracker.Services.Models;
using MedTracker.Services.Models.AppointmentsServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedTracker.Services.Interfaces
{
    public interface IAppointmentsService

    {
        public int TotalDoctorsFromSearch { get;  }
        public IEnumerable<DoctorSpecializationsCheckboxServiceModel> ListOfDoctorSpecializationsForCheckboxes();

        public  IEnumerable<DoctorResultFromCheckboxSearchInformationServiceModel> ResultFromSearchCheckboxesForDoctors(IEnumerable<DoctorSpecializationsCheckboxServiceModel> dscsm, int page = 1);
        public  DoctorFullDetailsServiceModel GetDoctorDetailsById(int id);

    }
}
