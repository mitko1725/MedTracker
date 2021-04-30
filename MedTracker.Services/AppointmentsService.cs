using MedTracker.Data;
using MedTracker.Services.Interfaces;
using MedTracker.Services.Models;
using MedTracker.Services.Models.AdminServiceModels;
using MedTracker.Services.Models.AppointmentsServiceModels;
using MedTracker.Services.Models.DoctorSpecializationServiceModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedTracker.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly MedTrackerDbContext data;
      

        public AppointmentsService(MedTrackerDbContext data)
        {
            this.data = data;

        }

        public int TotalDoctorsFromSearch { get;private set; }

        public DoctorFullDetailsServiceModel GetDoctorDetailsById(int id)
        {
            var doctor = this.data.Doctors.Where(x => x.Id == id).FirstOrDefault();

            if (doctor == null)
            {
                throw new NullReferenceException("There is not such doctor with that ID.");
            }
            var dfdsm = new DoctorFullDetailsServiceModel()
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                MiddleName = doctor.MiddleName,
                LastName = doctor.LastName,
                ProfilePic = doctor.ProfilePic,
                Gender = doctor.Gender,
                UserId = doctor.UserId,
                IsActive = doctor.IsActive,
                Biography = doctor.Biography,
                DoctorSpecializations = new List<DoctorSpecializationServiceModel>()
            };

            return dfdsm;
        }

        public IEnumerable<DoctorSpecializationsCheckboxServiceModel> ListOfDoctorSpecializationsForCheckboxes()
        => data.Specializations
            .AsNoTracking()
            .Select(x => new DoctorSpecializationsCheckboxServiceModel
            {
                SpecId = x.Id,
                Name = x.Name,
                Checked = false
            }).ToList();

        public  IEnumerable<DoctorResultFromCheckboxSearchInformationServiceModel> ResultFromSearchCheckboxesForDoctors(IEnumerable<DoctorSpecializationsCheckboxServiceModel> dscsm, int page = 1)
        {

            try
            {
                IList<DoctorResultFromCheckboxSearchInformationServiceModel> dssm = new List<DoctorResultFromCheckboxSearchInformationServiceModel>();
                var checkedDoctorSpecs = dscsm.Where(x => x.Checked).ToList();
                var doctorSpecializations = this.data.Doctor_Specialization.Select(x => new DoctorSpecializationServiceModel() { DoctorId = x.DoctorId, SpecializationId = x.SpecializationId }).ToList();
                foreach (var item in doctorSpecializations)
                {
                    if (checkedDoctorSpecs.Any(x => x.SpecId == item.SpecializationId))
                    {
                        var doctorInfo = this.GetDoctorDetailsById(item.DoctorId);
                        // does it check if doctor is active or not !!!!!!!!!
                        if (!dssm.Any(x => x.DoctorId == item.DoctorId) && doctorInfo.IsActive)
                        {
                            dssm.Add(new DoctorResultFromCheckboxSearchInformationServiceModel()
                            {
                                DoctorId = item.DoctorId,
                                FullName = doctorInfo.FirstName + " "+ doctorInfo.MiddleName +" "+ doctorInfo.LastName,
                                ProfilePicture = doctorInfo.ProfilePic,
                                DoctorSpecializations = new List<SpecializationDetailsServiceModel>()
                            });

                        }
                        if (doctorInfo.IsActive)
                        {
                            dssm.Where(x => x.DoctorId == item.DoctorId).FirstOrDefault().DoctorSpecializations
                              .Add(new SpecializationDetailsServiceModel()
                              {
                                  Id = item.SpecializationId,
                                  Name = this.data.Specializations.Where(x => x.Id == item.SpecializationId).FirstOrDefault().Name
                              });
                        }

                    }
                }
                TotalDoctorsFromSearch = dssm.Count();
                return dssm.Skip((page - 1) * 20).Take(20);
            }
            catch (Exception e)
            {

                throw new ArgumentException();
            }



        }



   
    }
}
