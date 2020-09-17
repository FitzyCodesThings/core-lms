using CoreLMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreLMS.Persistence
{    
    // TODO Update SelectCoursesAsync to use custom predicate/where clause

    public partial class AppDbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseLesson> CourseLessons { get; set; }
        public DbSet<CourseLessonAttachment> CourseLessonAttachments { get; set; }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            EntityEntry<Course> courseEntry = await this.Courses.AddAsync(course);
            await this.SaveChangesAsync();
            return courseEntry.Entity;
        }

        public async Task<Course> UpdateCourseAsync(Course course)
        {
            EntityEntry<Course> courseEntry = this.Courses.Update(course);
            await this.SaveChangesAsync();
            return courseEntry.Entity;
        }

        public async Task<Course> DeleteCourseAsync(Course course)
        {
            EntityEntry<Course> courseEntry = this.Courses.Remove(course);
            await this.SaveChangesAsync();
            return courseEntry.Entity;
        }

        public async Task<Course> SelectCourseByIdAsync(int id) =>
            await this.Courses
                .Include(p => p.CourseLessons)
                    .ThenInclude(p => p.CourseLessonAttachments)
                .Include(p => p.CourseLessons)
                    .ThenInclude(p => p.Authors)
                        .ThenInclude(p => p.Author)
                            .ThenInclude(p => p.Person)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<List<Course>> SelectCoursesAsync()
        {
            return await this.Courses
                            .Include(p => p.CourseLessons)
                                .ThenInclude(p => p.CourseLessonAttachments)
                            .Include(p => p.CourseLessons)
                                .ThenInclude(p => p.Authors)
                                    .ThenInclude(p => p.Author)
                                        .ThenInclude(p => p.Person)
                            .ToListAsync();
        }
    }
}
