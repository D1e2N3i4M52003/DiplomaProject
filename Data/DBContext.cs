using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using DataLayer.Models;
using BCryptNet = BCrypt.Net.BCrypt;

namespace DataLayer
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

       
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Destinations> Destinations { get; set; }
        public DbSet<Excursion> Excursions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();


            modelBuilder.Entity<Post>()
                .HasIndex(x => x.Title)
                .IsUnique();

            modelBuilder.Entity<Destinations>()
                .HasIndex(x => x.Name)
                .IsUnique();

            
            modelBuilder.Entity<User>().HasData(
                new User 
                { 
                    Id = Guid.NewGuid(),
                    Firstname = "Ad",
                    Lastname = "min",
                    Username = "Admin",
                    Email = "admin@gmail.com",
                    CreationDate = DateTime.Now,
                    Role = Role.Admin,
                    PasswordHash = BCryptNet.HashPassword("AdminPa33word")
                });

            modelBuilder.Entity<Destinations>().HasData(
                new Destinations
                {
                    Id = Guid.NewGuid(),
                    Name = "Ancient Theathre",
                    City = "Plovdiv",
                    Description = "In Plovdiv"
                });
            modelBuilder.Entity<Excursion>().HasData(
                new Excursion
                {
                    Id = Guid.NewGuid(),
                    Name = "Plovdiv",
                    CreationDate = DateTime.Now,
                    Price = 14,
                    StartsOnDate = DateTime.Today,
                    EndsOnDate = DateTime.Today,
                    Destinations = new List<Destinations>()
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}