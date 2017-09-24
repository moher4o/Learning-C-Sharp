using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OneToMany.Models;

namespace OneToMany
{
   public class MyDbContext : DbContext
    {
        public DbSet<Person> Employes { get; set; }

        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MYTestDatabase;Integrated security=True;");

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(d => d.Department)
                .WithMany(e => e.Employes)
                .HasForeignKey(d => d.DepartmentId);
            

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Manager)
                .WithMany(p => p.Subordinates)
                .HasForeignKey(p => p.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
