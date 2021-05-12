using System;

using System.ComponentModel.DataAnnotations;


namespace MedTracker.Models
{
   public class DoctorWorkingHours
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartHour { get; set; }

        [Required]
        public DateTime EndHour { get; set; }


        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

    }
}
