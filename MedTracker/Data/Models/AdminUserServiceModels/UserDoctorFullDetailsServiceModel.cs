using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedTracker.Web.Data.Models.AdminUserServiceModels
{
    public class UserDoctorFullDetailsServiceModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

    }
}
