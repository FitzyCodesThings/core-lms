using CoreLMS.Core.Entities;
using CoreLMS.Core.Interfaces;
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

        public async Task<List<Course>> GetCoursesAsync() => await db.SelectCoursesAsync();
    }
}
