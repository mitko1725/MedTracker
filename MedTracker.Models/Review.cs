using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MedTracker.Models
{
  public  class Review
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime ReviewDate { get; set; }
        [Required]
        public OverallRating OverallRating { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

    }
}
