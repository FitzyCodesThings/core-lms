using CoreLMS.Core.Entities;
using CoreLMS.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreLMS.Application.Services
{
    public class CourseService : ICourseService
    {   
        private readonly IAppDbContext db;

        public CourseService(IAppDbContext db)
        {   
            this.db = db;
        }

        public async Task<Course> GetCourseAsync(int id)
        {
            var course = await this.db.SelectCourseByIdAsync(id);

            if (course == null)               
                throw new ApplicationException($"Course {id} not found.");                

            return course;
        }

        public async Task<List<Course>> GetCoursesAsync() => await db.SelectCoursesAsync();
    }
}
