using MedTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedTracker.Services.Models
{
  public  class CreateUserServiceModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthdate{ get; set; }
        public Gender Gender{ get; set; }
        public Insured Insured { get; set; }

        public string ProfilePic { get; set; }
        public Guid UserId { get; set; }

    }
}
