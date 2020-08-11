using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementApp.Models {
    public class AppContext:DbContext {
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Posts> Postss { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Posts>().ToTable("Posts");
            }
        }
    }



    