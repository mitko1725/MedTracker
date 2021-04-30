using MedTracker.Services.Models.AppointmentsServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedTracker.Web.Models.Appointments
{
    public class DoctorsResultFromSearchViewModel
    {
        public IList<DoctorSpecializationsCheckboxServiceModel> DoctorSpecializationsCheckBoxes { get; set; }
        public IList<DoctorResultFromCheckboxSearchInformationServiceModel> Doctors { get; set; }

        public int CurrentPage { get; set; }
        public int Total { get; set; }
        public int PreviousPage => this.CurrentPage - 1;
        public int NextPage => this.CurrentPage + 1;

        public bool PreviousDisabled => this.CurrentPage == 1;
        public int MaxPage => (int)Math.Ceiling((double)this.Total / 20);
        public bool NextDisabled
        {
            get
            {
                var maxPage = Math.Ceiling((double)this.Total / 20);
                return maxPage == this.CurrentPage;
            }
        }


    }
}
