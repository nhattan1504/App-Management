using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementApp.Models {
    public class AppContext:DbContext {
        //public AppContext() { }
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Posts> Postss { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        //            if (!optionsBuilder.IsConfigured)
        //                {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Data Source = (LocalDB)\\MSSQLLocalDB; Initial Catalog = AppManagement2; Integrated Security = true; Persist Security Info = False; User ID = sa; Password = abc");
        //                }
        //            }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Posts>().ToTable("Posts");
            }
        }
    }



    