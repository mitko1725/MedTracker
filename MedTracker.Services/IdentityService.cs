using MedTracker.Data;
using MedTracker.Models;
using MedTracker.Services.Interfaces;
using MedTracker.Services.Models;
using MedTracker.Services.Models.IdentityServiceModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedTracker.Services
{
    public class IdentityService : IIdentityService
    {

        private readonly MedTrackerDbContext data;


        public IdentityService(MedTrackerDbContext data)
        {
            this.data = data;
        }

        public void AddNewSpecializationsForDoctor(List<int> model, int docId)
        {
            for (int i = 0; i < model.Count; i++)
            {
                this.data.Doctor_Specialization.Add(new Doctor_Specialization { DoctorId = docId, SpecializationId = model[i] });
            }
            data.SaveChanges();
        }

        public IEnumerable<ModelsForDoctorSpecializationsServiceModel> DoctorSpecializations(int docId)
        {

            var docSpecs = this.data.Doctor_Specialization
                 .Where(x => x.DoctorId == docId)
                 .Select(x => new ModelsForDoctorSpecializationsServiceModel
                 {
                     Id = x.Specialization.Id,
                     Name = x.Specialization.Name


                 }).ToList();
            return docSpecs;



        }

        public IEnumerable<ModelsForDoctorSpecializationsServiceModel> DoctorSpecializationThatCanBeAddedForSelect(IEnumerable<ModelsForDoctorSpecializationsServiceModel> currentSpecs)
        {
            //should take all specs
            var specializationsForSelect = new List<ModelsForDoctorSpecializationsServiceModel>();
            foreach (var item in data.Specializations.ToList())
            {
                if (!currentSpecs.Any(x => x.Name == item.Name))
                {
                    specializationsForSelect.Add(new ModelsForDoctorSpecializationsServiceModel { Id = item.Id, Name = item.Name });
                }
            }
            return specializationsForSelect;
        }

        public DoctorFullDetailsServiceModel GetDoctorDetails(Guid id)
        => this.data.Doctors
            .AsNoTracking()
            .Where(x => x.UserId == id)
            .Select(x => new DoctorFullDetailsServiceModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                Gender = x.Gender,
                Biography = x.Biography,
                ProfilePic = x.ProfilePic,
                UserId = id

            }).FirstOrDefault();

        public PatientFullDetails GetPatientDetails(Guid id)
       => this.data.Patients.AsNoTracking()
            .Where(x => x.UserId == id)
            .Select(x => new PatientFullDetails
            {
                Id = x.Id,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                Gender = x.Gender,
                Insured = x.Insured,
                ProfilePic = x.ProfilePic,
                BirthDate = x.Birthdate,
                UserId = id
            })
            .FirstOrDefault();

        public IEnumerable<ModelsForDoctorSpecializationsServiceModel> ModelsDoctorSelect()
     => this.data.Specializations
            .AsNoTracking()
            .Select(x => new ModelsForDoctorSpecializationsServiceModel
            {
                Id = x.Id,
                Name = x.Name

            }).ToList();

        public void RemoveDoctorSpecialization(int specId, int docId)
        {
            var itemToRemove = new Doctor_Specialization()
            {
                DoctorId = docId,
                SpecializationId = specId
            };
            this.data.Remove(itemToRemove);
            this.data.SaveChanges();
        }

        public void UpdateDoctorDetails(DoctorFullDetailsServiceModel model)
        {
            var doctor = data.Doctors.Where(x => x.UserId == model.UserId).FirstOrDefault();
            doctor.FirstName = model.FirstName;
            doctor.MiddleName = model.MiddleName;
            doctor.LastName = model.LastName;
            doctor.Gender = model.Gender;
            doctor.Biography = model.Biography;
            doctor.ProfilePic = model.ProfilePic;
            this.data.SaveChanges();
        }

        public void UpdatePatientDetails(PatientFullDetails model)
        {
            var patient = data.Patients.Where(x => x.UserId == model.UserId).FirstOrDefault();

            patient.FirstName = model.FirstName;
            patient.MiddleName = model.MiddleName;
            patient.LastName = model.LastName;
            patient.Gender = model.Gender;
            patient.Insured = model.Insured;
            patient.Birthdate = model.BirthDate;
            patient.ProfilePic = model.ProfilePic;
            this.data.SaveChanges();

        }
    }
}
