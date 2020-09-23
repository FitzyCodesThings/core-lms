using CoreLMS.Core.DataTransferObjects;
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
    public partial class CourseService : ICourseService
    {   
        private readonly IAppDbContext db;
        private readonly ILogger<CourseService> logger;

        public CourseService(IAppDbContext db, ILogger<CourseService> logger)
        {   
            this.db = db;
            this.logger = logger;
        }

        public async Task<Course> AddCourseAsync(CreateCourseDto courseDto)
        {
            var course = new Course
            {
                Name = courseDto.Name,
                Description = courseDto.Description,
                CourseType = courseDto.CourseType,
                CourseImageURL = courseDto.CourseImageURL
            };

            try
            {
                this.ValidateCourseOnCreate(course);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Attempted to add invalid course.");
                throw;
            }

            return await this.db.CreateCourseAsync(course);
        }

        public async Task<Course> UpdateCourseAsync(UpdateCourseDto courseDto)
        {
            var course = await db.SelectCourseByIdAsync(courseDto.Id);

            course.Name = courseDto.Name;
            course.Description = courseDto.Description;
            course.CourseType = courseDto.CourseType;
            course.CourseImageURL = courseDto.CourseImageURL;            

            try
            {
                this.ValidateCourseOnUpdate(course);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Attempted to update invalid course.");
                throw;
            }

            return await db.UpdateCourseAsync(course);
        }

        public async Task<Course> DeleteCourseAsync(int id)
        {
            var course = await db.SelectCourseByIdAsync(id);

            if (course == null)
            {
                logger.LogWarning($"Course {id} not found for deletion.");
                throw new ApplicationException($"Course {id} not found for deletion.");
            }

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
    }
}
