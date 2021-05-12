
using System.ComponentModel.DataAnnotations;


namespace MedTracker.Models
{
   public class DoctorWorkingDays
    {
        [Key]
        public int Id { get; set; }

        [Required]

        public bool Monday { get; set; }
        [Required]

        public bool Tuesday { get; set; }
        [Required]

        public bool Wednesday { get; set; }
        [Required]

        public bool Thursday { get; set; }
        [Required]

        public bool Friday { get; set; }
        [Required]

        public bool Saturday { get; set; }
        [Required]

        public bool Sunday { get; set; }


        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

    }
}
