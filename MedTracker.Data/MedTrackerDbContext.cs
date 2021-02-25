using MedTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MedTracker.Data
{
    public class MedTrackerDbContext :DbContext
    {
      
         public MedTrackerDbContext(DbContextOptions<MedTrackerDbContext> options)
        : base(options)
        {

            
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Doctor_Specialization> Doctor_Specialization { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Specialization> Specializations { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            builder.Entity<Doctor>(d => {
                d.HasKey(x => x.Id).HasName("PK_Doctors");

                //d.HasMany(da => da.Reviews)
                //.WithOne(da => da.Doctor)
                //.HasForeignKey(da => da.DoctorId);

            });





            builder.Entity<Review>(r => {
                r.HasKey(x => x.Id).HasName("PK_Reviews");

                r.HasOne(x => x.Doctor)
                .WithMany(d => d.Reviews)
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

                r.HasOne(x => x.Patient)
              .WithMany(d => d.Reviews)
             .HasForeignKey(x => x.PatientId)
             .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Appointment>(a => {
                a.HasKey(b => b.Id).HasName("PK_Appointments");

                a.HasOne(x => x.Doctor)
                 .WithMany(x => x.Appointments)
                 .HasForeignKey(x => x.DoctorId)
                 .OnDelete(DeleteBehavior.Restrict);


                a.HasOne(x => x.Patient)
                 .WithMany(x => x.Appointments)
                 .HasForeignKey(x => x.PatientId)
                 .OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<Doctor_Specialization>(ds =>
            {



                ds.HasKey(d => new { d.DoctorId, d.SpecializationId });

                ds.HasOne(d => d.Doctor)
                .WithMany(d => d.DoctorSpecializations)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

                ds.HasOne(s => s.Specialization)
                .WithMany(s => s.DoctorSpecialization)
                .HasForeignKey(s => s.SpecializationId)
                .OnDelete(DeleteBehavior.Restrict);


            });
        }

    }
}
