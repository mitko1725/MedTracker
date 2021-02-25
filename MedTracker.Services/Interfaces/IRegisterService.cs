﻿using MedTracker.Models;
using MedTracker.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedTracker.Services.Interfaces
{
   public interface IRegisterService
    {
        public void CreateUser(CreateUserServiceModel model);
        public void CreateDoctor(CreateDoctorServiceModel model);
        public DoctorFullDetailsServiceModel GetDoctorByUserId(Guid userId);
    }
}