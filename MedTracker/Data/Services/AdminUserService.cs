using MedTracker.Data;
using MedTracker.Services.Models;
using MedTracker.Web.Data.Models.AdminUserServiceModels;
using MedTracker.Web.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedTracker.Web.Data.Services
{
    public class AdminUserService : IAdminUserService
    {
   
        private readonly MedTrackerDbContext _data;
        private readonly ApplicationDbContext _userData;
        public AdminUserService(
       MedTrackerDbContext data, ApplicationDbContext userData)
        {
     
            _data = data;
            _userData = userData;
        }

        public UserDoctorFullDetailsServiceModel FindUserDoctorById(DoctorFullDetailsServiceModel model)
      => this._userData.Users
                    .Select(x => new UserDoctorFullDetailsServiceModel
                    {
                        Id = x.Id,
                        UserName = x.UserName,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber
                    })
                    .Where(x => x.Id == model.UserId)
                    .FirstOrDefault();
         
        

        public IEnumerable<UserDoctorFullDetailsServiceModel> NotApprovedUserDoctors(IEnumerable<DoctorFullDetailsServiceModel> model)
        {
            List<UserDoctorFullDetailsServiceModel> list = new List<UserDoctorFullDetailsServiceModel>();
            foreach (var user in model)
            {
                list.Add(this._userData.Users
                    .Select(x => new UserDoctorFullDetailsServiceModel
                    {
                        Id = x.Id,
                        UserName = x.UserName,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber
                    })
                    .Where(x => x.Id == user.UserId)
                    .FirstOrDefault());
            }
            return list;
        }

        public void RejectUserDoctor(DoctorFullDetailsServiceModel model)
        {
            var getDoctor = _data.Doctors.Where(x => x.UserId == model.UserId).FirstOrDefault();
            var getUserDoctor = _userData.Users.Where(x => x.Id == model.UserId).FirstOrDefault();
            var getUserRolesDoctor = _userData.UserRoles.Where(x => x.UserId == model.UserId).FirstOrDefault();

            _data.Remove(getDoctor);
            foreach (var doctorSpecialization in model.DoctorSpecializations)
            {
                var docSpecEntity = _data.Doctor_Specialization.Where(x => (x.DoctorId == doctorSpecialization.DoctorId) && (x.SpecializationId == doctorSpecialization.SpecializationId)).FirstOrDefault();
                _data.Remove(docSpecEntity);
            }
            // need to remove doctor specs first if any
            _userData.Remove(getUserDoctor);
            _userData.Remove(getUserRolesDoctor);
            _data.SaveChanges();
            _userData.SaveChanges();

        }
    }
}
