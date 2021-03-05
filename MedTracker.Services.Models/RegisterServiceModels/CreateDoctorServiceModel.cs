using MedTracker.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedTracker.Services.Models
{
   public class CreateDoctorServiceModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
    
        public Gender Gender { get; set; }
        public string Biography{ get; set; }

        public string ProfilePic { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
       


    }
}
