using MedTracker.Services.Models;
using MedTracker.Services.Models.AdminServiceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedTracker.Services.Interfaces
{
   public interface IAdminService
    {
        public IEnumerable<DoctorFullDetailsServiceModel> NotApprovedDoctors(int page=1);
        public IEnumerable<SpecializationDetailsServiceModel> AllSpecializations(int page=1);
        public DoctorFullDetailsServiceModel FindDoctorById(Guid Id);
        public void MakeDoctorActive(DoctorFullDetailsServiceModel model);
        public void AddSpecialization(string specName);
        public void RemoveSpecialization(int id);


        public int TotalNotActive();
        public int TotalSpecializations();
    }
}
