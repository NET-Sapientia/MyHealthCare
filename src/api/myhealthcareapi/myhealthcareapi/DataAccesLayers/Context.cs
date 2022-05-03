using Microsoft.EntityFrameworkCore;
using myhealthcareapi.DataAccesLayers.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.DataAccesLayers
{
    public class Context : DbContext
    {
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<MedicEntity> Medics { get; set; }
        public DbSet<HospitalEntity> Hospitals{ get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<MedicDepartmentEntity> MedicDepartments { get; set; }
        public DbSet<AppointmentEntity> Appointments { get; set; }
        public DbSet<FeedBackEntity> FeedBacks { get; set; }
        public Context([NotNullAttribute] DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MedicDepartmentEntity>()
                .HasKey(md => new { md.MedicId, md.DepartmentId });

            modelBuilder.Entity<MedicDepartmentEntity>()
                .HasOne<MedicEntity>(md => md.Medic)
                .WithMany(m => m.MedicDepartments)
                .HasForeignKey(md => md.MedicId);

            modelBuilder.Entity<MedicDepartmentEntity>()
                .HasOne<DepartmentEntity>(md => md.Department)
                .WithMany(m => m.MedicDepartments)
                .HasForeignKey(md => md.DepartmentId);

        }
    }
}
