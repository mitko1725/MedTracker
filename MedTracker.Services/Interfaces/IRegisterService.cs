using MedTracker.Models;
using MedTracker.Services.Models;
using MedTracker.Services.Models.IdentityServiceModels;
using MedTracker.Services.Models.RegisterServiceModels;
using System;


namespace MedTracker.Services.Interfaces
{
   public interface IRegisterService
    {
        public void CreateUser(CreateUserServiceModel model);
        public void CreateDoctor(CreateDoctorServiceModel model);
        public DoctorFullDetailsServiceModel GetDoctorByUserId(Guid userId);
        public PatientFullDetails GetPatientByUserId(Guid userId);
        public void AddDoctorSpecializations(DoctorSpecializationsServiceModel model);
    }
}
