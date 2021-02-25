using MedTracker.Services.Models;
using MedTracker.Web.Data.Models.AdminUserServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedTracker.Web.Areas.Admin.ViewModels
{
    public class NotActiveDoctorsViewModel
    {
        public IEnumerable<DoctorFullDetailsServiceModel> Doctors { get; set; }
        public IEnumerable<UserDoctorFullDetailsServiceModel> UserDoctors { get; set; }
        public int CurrentPage { get; set; }
        public int Total { get; set; }
        public int PreviousPage
        {
            get 
            { 
              return  this.CurrentPage - 1;
            }
        
        }
        public int NextPage => this.CurrentPage + 1;

        public bool PreviousDisabled {
            get { return this.CurrentPage == 1; }
           
        }
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
