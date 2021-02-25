using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MedTracker.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string MiddleName { get; set; }
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        public string ProfilePic { get; set; }
        [Required]
        public Gender Gender{ get; set; }

        public string Biography { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Doctor_Specialization> DoctorSpecializations { get; set; } = new HashSet<Doctor_Specialization>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

    }
}
