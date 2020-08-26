using CoreLMS.Core.Entities;
using CoreLMS.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLMS.Application.Services
{
    public class CourseService : ICourseService
    {   
        private readonly IAppDbContext db;
        private readonly ILogger<CourseService> logger;

        public CourseService(IAppDbContext db, ILogger<CourseService> logger)
        {   
            this.db = db;
            this.logger = logger;
        }

        public async Task<Course> AddCourseAsync(Course course)
        {
            return await this.db.CreateCourseAsync(course);
        }

        public async Task<Course> DeleteCourseAsync(Course course)
        {
            return await this.db.DeleteCourseAsync(course);
        }

        public async Task<Course> GetCourseAsync(int id)
        {
            var course = await this.db.SelectCourseByIdAsync(id);

            if (course == null)
            {
                logger.LogWarning($"Course {id} not found.");
                throw new ApplicationException($"Course {id} not found.");
            }

            return course;
        }

        public async Task<List<Course>> GetCoursesAsync() => await db.SelectCoursesAsync();

        public async Task<Course> UpdateCourseAsync(Course course)
        {
            return await db.UpdateCourseAsync(course);
        }
    }
}
