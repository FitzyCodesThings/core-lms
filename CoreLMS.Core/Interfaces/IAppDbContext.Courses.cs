using CoreLMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreLMS.Core.Interfaces
{
    public partial interface IAppDbContext
    {
        public Task<Course> CreateCourseAsync(Course course);
        public Task<Course> SelectCourseByIdAsync(int id);
        public Task<List<Course>> SelectCoursesAsync();
        public Task<Course> UpdateCourseAsync(Course course);
        public Task<Course> DeleteCourseAsync(Course course);
    }
}
