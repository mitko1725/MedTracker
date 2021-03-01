﻿using MedTracker.Data;
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

        public DoctorFullDetailsServiceModel GetDoctorDetails(Guid id)
        => this.data.Doctors
            .AsNoTracking()
            .Where(x => x.UserId == id)
            .Select(x => new DoctorFullDetailsServiceModel
            {
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
            .Select(x => new PatientFullDetails {
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                Gender = x.Gender,
                Insured=x.Insured,
                ProfilePic=x.ProfilePic,
                BirthDate=x.Birthdate,
                UserId=id
            })
            .FirstOrDefault();

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

        public  void UpdatePatientDetails(PatientFullDetails model)
        {
            var patient = data.Patients.Where(x => x.UserId==model.UserId).FirstOrDefault();
         
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
