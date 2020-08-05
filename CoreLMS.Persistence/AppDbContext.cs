using CoreLMS.Core.Entities;
using CoreLMS.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreLMS.Persistence
{
    public partial class AppDbContext : DbContext, IAppDbContext
    {
        private readonly IConfiguration configuration;

        DbSet<Person> People { get; set; }
        DbSet<AuthorCourseLesson> AuthorCourseLessons { get; set; }

        public AppDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO Define indexes 

            #region Many to Manys
            modelBuilder.Entity<AuthorCourseLesson>()
                .HasKey(p => new { p.AuthorId, p.CourseLessonId });

            modelBuilder.Entity<AuthorCourseLesson>()
                .HasOne(p => p.Author)
                .WithMany(p => p.CourseLessons)
                .HasForeignKey(p => p.AuthorId);
            
            modelBuilder.Entity<AuthorCourseLesson>()
                .HasOne(p => p.CourseLesson)
                .WithMany(p => p.Authors)
                .HasForeignKey(p => p.CourseLessonId);
            #endregion

            #region Soft Deletes
            // TODO Investigate using IAuditableEntity
            modelBuilder.Entity<Course>().HasQueryFilter(e => e.DateDeleted == null);
            modelBuilder.Entity<CourseLesson>().HasQueryFilter(e => e.DateDeleted == null);
            modelBuilder.Entity<CourseLessonAttachment>().HasQueryFilter(e => e.DateDeleted == null);
            modelBuilder.Entity<Author>().HasQueryFilter(e => e.DateDeleted == null);
            modelBuilder.Entity<Person>().HasQueryFilter(e => e.DateDeleted == null);
            #endregion
        }
    }
}
