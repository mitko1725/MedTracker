using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MedTracker.Models
{
  public  class Specialization
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        public ICollection<Doctor_Specialization> DoctorSpecialization { get; set; } = new HashSet<Doctor_Specialization>();
    }
}
