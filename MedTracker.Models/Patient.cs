using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MedTracker.Models
{
  public  class Patient
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
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public Insured Insured { get; set; }
        public string ProfilePic { get; set; }
        public Guid UserId { get; set; }

        public ICollection<Review> Reviews{ get; set; } = new HashSet <Review>();
        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
