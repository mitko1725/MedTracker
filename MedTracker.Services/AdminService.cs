﻿using MedTracker.Data;
using MedTracker.Models;
using MedTracker.Services.Models;
using MedTracker.Services.Models.AdminServiceModels;
using MedTracker.Services.Models.DoctorSpecializationServiceModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedTracker.Services.Interfaces
{
    public class AdminService : IAdminService
    {
        private readonly MedTrackerDbContext data;
     
        public AdminService(MedTrackerDbContext data)
        {
            this.data = data;
        }

        public void AddSpecialization(string specName)
        {
            if (this.data.Specializations.Any(x=>x.Name== specName))
            {
                throw new ArgumentException("Cannot add Specialization with same name");
            }
            if (string.IsNullOrEmpty(specName))
            {
                throw new ArgumentException("Value cannot be null");
            }
            var spec = new Specialization()
            {
               
                Name = specName

            };
            data.Add(spec);
            data.SaveChanges();
        }

        public IEnumerable<SpecializationDetailsServiceModel> AllSpecializations(int page = 1)
        => this.data.Specializations.Skip((page - 1) * 20).Take(20)
                .AsNoTracking()
                .Select(c => new SpecializationDetailsServiceModel
                {
                 Id=c.Id,
                 Name=c.Name
                })
                .ToList();

        public IEnumerable<SpecializationDetailsServiceModel> DoctorSpecializations(int docId)
        => this.data.Doctor_Specialization.Where(x => x.DoctorId == docId).Select(x => new SpecializationDetailsServiceModel()
        {
        Id=x.DoctorId,
        Name=x.Specialization.Name

        }).ToList();

        public DoctorFullDetailsServiceModel FindDoctorById(Guid Id)
        => this.data.Doctors.Where(x => x.UserId == Id).Select(x => new DoctorFullDetailsServiceModel
        {
            Id=x.Id,
            FirstName = x.FirstName,
            MiddleName = x.MiddleName,
            LastName = x.LastName,
            Gender = x.Gender,
            Biography = x.Biography,
            ProfilePic = x.ProfilePic,
            UserId = x.UserId
        })
            .AsNoTracking()
            .FirstOrDefault();

        public IEnumerable<DoctorSpecializationServiceModel> ListOfDoctorSpecializations(int docId)
        => this.data.Doctor_Specialization.Where(x => x.DoctorId == docId).Select(x => new DoctorSpecializationServiceModel
        {
            DoctorId=x.DoctorId,
            SpecializationId=x.SpecializationId

        })
            .AsNoTracking()
            .ToList();

        public void MakeDoctorActive(DoctorFullDetailsServiceModel model)
        {
            var getDoctor = data.Doctors.Where(x => x.UserId == model.UserId).FirstOrDefault();
            getDoctor.IsActive = true;
            this.data.SaveChanges();
        }

        public IEnumerable<DoctorFullDetailsServiceModel> NotApprovedDoctors(int page = 1)
        {
            return this.data.Doctors
                .Where(x => x.IsActive == false)
                .Skip((page - 1) * 20)
                .Take(20)
                .AsNoTracking()
                .Select(c => new DoctorFullDetailsServiceModel
                {
                    FirstName = c.FirstName,
                    MiddleName = c.MiddleName,
                    LastName = c.LastName,
                    Gender = c.Gender,
                    UserId = c.UserId,
                })
                .ToList();
                
        }

        public void RemoveSpecialization(int id)
        {
            var getSpec = this.data.Specializations.Where(x => x.Id == id).FirstOrDefault();
            data.Remove(getSpec);
            data.SaveChanges();
        }

        public int TotalNotActive()
          => this.data.Doctors.Where(x => x.IsActive == false).Count();

        public int TotalSpecializations()
       => this.data.Specializations.Count();
    }
}
