using MedTracker.Services.Models;
using MedTracker.Services.Models.IdentityServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedTracker.Services.Interfaces
{
   public interface IIdentityService
    {
        public DoctorFullDetailsServiceModel GetDoctorDetails(Guid id);
        public PatientFullDetails GetPatientDetails(Guid id);

        public void UpdatePatientDetails(PatientFullDetails model);
        public void UpdateDoctorDetails(DoctorFullDetailsServiceModel model);
        public void RemoveDoctorSpecialization(int specId,int docId );
        public void AddNewSpecializationsForDoctor(List<int> model,int docId);

        public IEnumerable<ModelsForDoctorSpecializationsServiceModel> ModelsDoctorSelect();
        public IEnumerable<ModelsForDoctorSpecializationsServiceModel> DoctorSpecializationThatCanBeAddedForSelect(IEnumerable<ModelsForDoctorSpecializationsServiceModel> currentSpecs);
        public IEnumerable<ModelsForDoctorSpecializationsServiceModel> DoctorSpecializations(int doctorId);

    } 
}
