
using MedTracker.Services.Models;
using MedTracker.Web.Data.Models.AdminUserServiceModels;
using System.Collections.Generic;


namespace MedTracker.Web.Data.Services.Interfaces
{
   public interface IAdminUserService
    {
        public IEnumerable<UserDoctorFullDetailsServiceModel> NotApprovedUserDoctors(IEnumerable<DoctorFullDetailsServiceModel> model);
        public UserDoctorFullDetailsServiceModel FindUserDoctorById(DoctorFullDetailsServiceModel model);
        public void RejectUserDoctor(DoctorFullDetailsServiceModel model);
    }
}
