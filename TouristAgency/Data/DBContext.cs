/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using TouristAgency.Models;

namespace TouristAgency.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public static DBContext Create() => new DBContext();
        /*
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Destinations> Destinations { get; set; }
        public DbSet<Excursion> Excursions { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(Option  modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder
                .Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorId);

            modelBuilder
                .Entity<Section>()
                .HasMany(s => s.Posts);

            modelBuilder
                .Entity<Post>()
                .HasMany(p => p.Photos);

            modelBuilder
                .Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(p => p.Parent)
                .HasForeignKey(p => p.ParentId);

            modelBuilder
                .Entity<Like>()
                .HasKey(l => new { l.UserId, l.PostId });

            base.OnModelCreating(modelBuilder);
        }

    }
}*/