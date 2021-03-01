using MedTracker.Services.Models;
using MedTracker.Services.Models.IdentityServiceModels;
using System;


namespace MedTracker.Services.Interfaces
{
   public interface IIdentityService
    {
        public DoctorFullDetailsServiceModel GetDoctorDetails(Guid id);
        public PatientFullDetails GetPatientDetails(Guid id);

        public void UpdatePatientDetails(PatientFullDetails model);
        public void UpdateDoctorDetails(DoctorFullDetailsServiceModel model);

    } 
}
