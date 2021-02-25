using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedTracker.Data
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        [MaxLength(30)]
    
        public string FirstName { get; set; }
        
        [MaxLength(30)]
       
        public string LastName { get; set; }

       


    }

}
