using MedTracker.Models;
using MedTracker.Services.Models.DoctorSpecializationServiceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedTracker.Services.Models
{
   public class DoctorFullDetailsServiceModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public Gender Gender { get; set; }
        public string Biography { get; set; }

        public string ProfilePic { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }

        public List<DoctorSpecializationServiceModel> DoctorSpecializations { get; set; }





    }
}
