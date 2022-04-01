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
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Destinations> Destinations { get; set; }
        public DbSet<Excursion> Excursions { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder
                .Entity<Like>()
                .HasKey(l => new { l.UserId, l.PostId });

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

            base.OnModelCreating(modelBuilder);
        }
 
    }
}