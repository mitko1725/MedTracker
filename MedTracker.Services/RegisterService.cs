﻿using MedTracker.Data;
using MedTracker.Models;
using MedTracker.Services.Interfaces;
using MedTracker.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedTracker.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly MedTrackerDbContext data;
        public RegisterService(MedTrackerDbContext data)
        {
            this.data = data;
        }

        public void CreateDoctor(CreateDoctorServiceModel model)
        {
            var doctor = new Doctor()
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Biography = model.Biography,
                Gender = model.Gender,
                IsActive = false,
                ProfilePic = model.ProfilePic,
                UserId = model.UserId

            };
            this.data.Add(doctor);
            this.data.SaveChanges();
        }

        public void CreateUser(CreateUserServiceModel model)
        {
            var patient = new Patient()
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Birthdate = model.Birthdate,
                Gender = model.Gender,
                Insured = model.Insured,
                ProfilePic = model.ProfilePic,
                UserId = model.UserId

            };
            this.data.Add(patient);
            this.data.SaveChanges();
        }

        public DoctorFullDetailsServiceModel GetDoctorByUserId(Guid userId)
        {
            var doctor = this.data.Doctors.AsNoTracking().Where(x => x.UserId == userId).FirstOrDefault();
            if (doctor == null)
            {
                throw new ArgumentException("No such doctor");
             


                
            }
            var doctorUserDetails = new DoctorFullDetailsServiceModel()
            {
                FirstName = doctor.FirstName,
                MiddleName = doctor.MiddleName,
                LastName = doctor.LastName,
                Biography = doctor.Biography,
                Gender = doctor.Gender,
                IsActive = doctor.IsActive,
                ProfilePic = doctor.ProfilePic,
                UserId = doctor.UserId
            };
            return doctorUserDetails;
        }

    }
}